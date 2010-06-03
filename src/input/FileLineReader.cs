// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// The copyright holder licenses this file under the New (3-clause) BSD 
// License (the "License").  You may not use this file except in 
// compliance with the License.  A copy of the License is available at 
// 
//   http://www.opensource.org/licenses/bsd-license.php 
// 
// and is included in the NOTICE.txt file distributed with this work.
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System.IO;

namespace Edu.Wisc.Forest.Flel.Util
{
    public class FileLineReader
        : LineReader
    {
        private string path;
        private StreamReader reader;

        //---------------------------------------------------------------------

        public string Path
        {
            get {
                return path;
            }
        }

        //---------------------------------------------------------------------

        public override string SourceName
        {
            get {
                return "file \"" + path + "\"";
            }
        }

        //---------------------------------------------------------------------

        public FileLineReader(string path)
            : base()
        {
            this.path = path;
            this.reader = new StreamReader(path);
        }

        //---------------------------------------------------------------------

        protected override string GetNextLine()
        {
            return reader.ReadLine();
        }

        //---------------------------------------------------------------------

        public override void Close()
        {
            base.Close();
            if (reader != null) {
                reader.Close();
                reader = null;
            }
        }
    }
}
