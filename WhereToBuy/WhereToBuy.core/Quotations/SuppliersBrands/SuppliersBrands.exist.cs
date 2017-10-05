using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.core
{
    public partial class SuppliersBrands
    {

        public bool Exists(Supplier supplier, Brand brand,ref string info)
        {

            try
            {

                if (engine.Data.SuppliersBrands.Exists(supplier, brand))
                {
                    return true;
                }

                info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("SupplierBrandTrustString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                return false;

               
            }
            catch (MyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Exists(string supplierCode, string brandCode, ref string info)
        {

            try
            {

                if (engine.Data.SuppliersBrands.Exists(supplierCode, brandCode))
                {
                    return true;
                }

                info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("SupplierBrandTrustString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                return false;

                
            }
            catch (MyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
