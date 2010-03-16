using System.IO;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// A text-based progress bar for console interfaces.
    /// </summary>
    public class ProgressBar
    {
        public const char BarChar = '+';

        private uint workToDo;
        private uint workDone;
        private float percentageDone;
        private uint barLength;
        private TextWriter writer;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="workUnitsToDo">
        /// Total number of work units to be done.
        /// </param>
        /// <param name="writer">
        /// Where the progress bar is displayed.
        /// </param>
        public ProgressBar(uint       workUnitsToDo,
                           TextWriter writer)
        {
            this.workToDo = workUnitsToDo;
            this.workDone = 0;
            this.percentageDone = 0.0f;
            this.barLength = 0;

            this.writer = writer;
            writer.WriteLine("% done:   0%  10%  20%  30%  40%  50%  60%  70%  80%  90%  100%");
            writer.WriteLine("          |----|----|----|----|----|----|----|----|----|----|");
            writer.Write(    "Progress: ");

            UpdateBar();
        }

        //---------------------------------------------------------------------

        public void IncrementWorkDone(uint increment)
        {
            workDone += increment;
            percentageDone = workDone / (float)workToDo * 100.0f;
            UpdateBar();
        }

        //---------------------------------------------------------------------

        private void UpdateBar()
        {
            uint newBarLength = ComputeBarLength(percentageDone);
            if (newBarLength > barLength) {
                uint deltaLength = (uint) (newBarLength - barLength);
                for (int i = 1; i <= deltaLength; i++)
                    writer.Write(BarChar);
                barLength = newBarLength;
            }
            if (percentageDone >= 100)
                Done();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Computes the length of the progress bar.
        /// </summary>
        /// <returns>
        /// The length is the number of characters that the bar should be.
        /// </returns>
        /// <remarks>
        /// Each character in the bar represents 2% of the total work to be
        /// done.  So the length is the percentage done divided by 2% (plus 1
        /// for the character underneath the "0%" spot).
        /// </remarks>
        public uint ComputeBarLength(float percentageDone)
        {
            return ((uint) percentageDone) / 2 + 1;
        }

        //---------------------------------------------------------------------

        public void Done()
        {
            writer.WriteLine();
        }
    }
}
