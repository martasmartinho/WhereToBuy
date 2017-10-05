using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public static class ArrayManagement
    {
        #region Arrays

        public static int DaNumeroOcorrencias(int[] array, int valor)
        {
            int num = 0;
            for (int index = 0; index < array.Length; ++index)
            {
                if (array[index] == valor)
                    ++num;
            }
            if (num == 0)
                return -1;
            return num;
        }

        public static int DaNumeroOcorrencias(string[] array, string valor)
        {
            int num = 0;
            for (int index = 0; index < array.Length; ++index)
            {
                if (array[index] == valor)
                    ++num;
            }
            if (num == 0)
                return -1;
            return num;
        }

        public static void Ordena(ref string[] array, TiposOrdenacao tipoOrdenacao)
        {
            switch (tipoOrdenacao)
            {
                case TiposOrdenacao.Ascendente:
                    for (int index1 = 0; index1 < array.Length - 1; ++index1)
                    {
                        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
                        {
                            if (array[index2].CompareTo(array[index1]) < 0)
                            {
                                string str = array[index1];
                                array[index1] = array[index2];
                                array[index2] = str;
                            }
                        }
                    }
                    break;
                case TiposOrdenacao.Descendente:
                    for (int index1 = 0; index1 < array.Length - 1; ++index1)
                    {
                        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
                        {
                            if (array[index2].CompareTo(array[index1]) > 0)
                            {
                                string str = array[index1];
                                array[index1] = array[index2];
                                array[index2] = str;
                            }
                        }
                    }
                    break;
                default:
                    throw new Exception("Não implementado");
            }
        }

        public static void Ordena(ref int[] array, TiposOrdenacao tipoOrdenacao)
        {
            switch (tipoOrdenacao)
            {
                case TiposOrdenacao.Ascendente:
                    for (int index1 = 0; index1 < array.Length - 1; ++index1)
                    {
                        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
                        {
                            if (array[index2] < array[index1])
                            {
                                int num = array[index1];
                                array[index1] = array[index2];
                                array[index2] = num;
                            }
                        }
                    }
                    break;
                case TiposOrdenacao.Descendente:
                    for (int index1 = 0; index1 < array.Length - 1; ++index1)
                    {
                        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
                        {
                            if (array[index2] > array[index1])
                            {
                                int num = array[index1];
                                array[index1] = array[index2];
                                array[index2] = num;
                            }
                        }
                    }
                    break;
                default:
                    throw new Exception("Não implementado");
            }
        }

        public static void AdicionaElemento(ref string[] array)
        {
            string[] strArray = new string[array.Length + 1];
            for (int index = 0; index < array.Length; ++index)
                strArray[index] = array[index];
            array = strArray;
        }

        public static void AdicionaElemento(ref int[] array)
        {
            int[] numArray = new int[array.Length + 1];
            for (int index = 0; index < array.Length; ++index)
                numArray[index] = array[index];
            array = numArray;
        }

        public enum TiposOrdenacao
        {
            Ascendente,
            Descendente,
        }

        #endregion
    }
}
