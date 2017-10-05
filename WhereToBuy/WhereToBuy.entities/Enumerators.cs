using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WhereToBuy.entities
{
    public enum DataState
    { Active, Inactive, All, None }

    public enum DataType
    { New, Changed, All, None }

    public enum DataReset
    { OneDay, TwoDay, ThreeDay, FourDay, FiveDay, SixDay, OneWeek, TwoWeek, ThreeWeek, OneMonth, TwoMonth }

    public enum ProductFilter
    { Current, ManualUpdate, IPCGreaterZero, Discontinued, Inactive, All }
}
