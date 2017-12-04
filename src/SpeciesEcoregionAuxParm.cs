using Landis.Core;
using Landis.Library.Parameters;
using Landis.Library.Parameters.Ecoregions;

namespace Landis.Utilities
{
   public class SpeciesEcoregionAuxParm<T>
    {
       Species.AuxParm<Landis.Library.Parameters.Ecoregions.AuxParm<T>> values;
       ///<Summary>
       /// Gets a species and ecoregion specific value
       ///</Summary>
        public T this[ISpecies species, IEcoregion ecoregion]
        {
            get
            {
                return values[species][ecoregion];
            }

            set
            {
                values[species][ecoregion] = value;
            }
        }
        ///<Summary>
        /// Initializes a species and ecoregion specific parameter
        ///</Summary>
        public SpeciesEcoregionAuxParm(ISpeciesDataset speciesDataset, IEcoregionDataset ecoregionDataset)
        {
            values = new Species.AuxParm<Landis.Library.Parameters.Ecoregions.AuxParm<T>>(speciesDataset);
            foreach (ISpecies species in speciesDataset)
            {
                values[species] = new Landis.Library.Parameters.Ecoregions.AuxParm<T>(ecoregionDataset);
            }
        }
    }
}
