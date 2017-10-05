using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Groups
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        public void Store(Group group)
        {

            if (group.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Groups.Store(group);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        public void Delete(Group group)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Groups.Delete(group);
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
