using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using System.Reflection;
using System.Resources;
using System.Globalization;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.unittests
{
    class Program
    {
        
        static CoreEngine engine;

        static void Main(string[] args)
        {
            
         
            string connectionstring = "Server=DESKTOP-BH33CFC;Database=WhereToBuy;Trusted_Connection=true; Language = Portuguese";
            engine =  new CoreEngine(connectionstring);
            try
            {
                GlobalVariables.Language = engine.Languages.Get("pt-PT");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex);
                Console.ReadKey();
                return;
            }
            GlobalVariables.Culture = new CultureInfo(GlobalVariables.Language.Code);
            connectionstring = string.Format("Server=DESKTOP-BH33CFC;Database=WhereToBuy;Trusted_Connection=true; Language = {0}", GlobalVariables.Language.Description);
            engine =  new CoreEngine(connectionstring);
            

            #region Brands

            //NewBrand();
            //GetBrand();
            //GetBrands();
            //UpdateBrand();
            //DeleteBrand();
            //ExistBrand();
          

            #endregion

            #region User

            //NewUser();
            //GetUser();
            //GetUsers();
            //UpdateUser();
            //DeleteUser();
            //ExistUser();


            #endregion

            #region WarningType

            //NewWarningType();
            //GetWarningType();
            //GetWarningTypes();
            //UpdateWarningType();
            //DeleteWarningType();
            //ExistWarningType();

            #endregion

            #region Idiomas

            //NewLanguage();
            //GetLanguage();
            //GetLanguages();
            //UpdateLanguage();
            //DeleteLanguage();
            //ExistLanguage();

            #endregion

            #region Suppliers

            //NewSupplier();
            //GetSupplier();
            //GetSuppliers();
            //UpdateSupplier();
            //DeleteSupplier();
            //ExistSupplier();

            #endregion

            #region Categories

            //NewCategory();
            //GetCategory();
            //GetCategories();
            //UpdateCategory();
            //DeleteCategory();
            //ExistCategory();

            #endregion

            #region Supplements

            //NewSupplement();
            //GetSupplement();
            //GetSupplements();
            //UpdateSupplement();
            //DeleteSupplement();
            //ExistSupplement();

            #endregion

            #region States

            //NewState();
            //GetState();
            //GetStates();
            //UpdateState();
            //DeleteState();
            //ExistState();

            #endregion

            #region Taxes

            //NewTax();
            //GetTax();
            //GetTaxes();
            //UpdateTax();
            //DeleteTax();
            //ExistTax();

            #endregion

            #region WorryingTerms

            //NewWorryingTerm();
            //GetWorryingTerm();
            //GetWorryingTerms();
            //UpdateWorryingTerm();
            //DeleteWorryingTerm();
            //ExistWorryingTerm();

            #endregion

            #region Groups

            //NewGroup();
            //GetGroup();
            //GetGroups();
            //UpdateGroup();
            //DeleteGroup();
            //ExistGroup();


            #endregion

            #region Catalogs

            //NewCatalog();
            //GetCatalog();
            //GetCatalogs();
            //UpdateCatalog();
            //DeleteCatalog();
            //ExistCatalog();


            #endregion

            #region Classes

            //NewClasse();
            //GetClasse();
            //GetClasses();
            //UpdateClasse();
            //DeleteClasse();
            //ExistClasse();


            #endregion

            #region BrandMatching

            //NewBrandMatching();
            //GetBrandMatching();
            //GetBrandsMatching();
            //UpdateBrandMatching();
            //DeleteBrandMatching();
            //ExistBrandMatching();


            #endregion

            #region CategoriesMatching

            //NewCategoryMatching();
            //GetCategoryMatching();
            //GetCategoriesMatching();
            //UpdateCategoryMatching();
            //DeleteCategoryMatching();
            //ExistCategoryMatching();


            #endregion

            #region StatesMatching

            //NewStateMatching();
            //GetStateMatching();
            //GetStatesMatching();
            //UpdateStateMatching();
            //DeleteStateMatching();
            //ExistStateMatching();


            #endregion

            #region TaxesMatching

            //NewTaxMatching();
            //GetTaxMatching();
            //GetTaxesMatching();
            //UpdateTaxMatching();
            //DeleteTaxMatching();
            //ExistTaxMatching();


            #endregion

            #region SupplementsMatching

            //NewSupplementMatching();
            //GetSupplementMatching();
            //GetSupplementsMatching();
            //UpdateSupplementMatching();
            //DeleteSupplementMatching();
            //ExistSupplementMatching();


            #endregion

            #region SuppliersBrands

            //NewSupplierBrand();
            //GetSupplierBrand();
            //GetSuppliersBrands();
            //UpdateSupplierBrand();
            //DeleteSupplierBrand();
            //ExistSupplierBrand();


            #endregion

            #region StocksMatching

            //NewStockMatching();
            //GetStockMatching();
            //GetStocksMatching();
            //UpdateStockMatching();
            //DeleteStockMatching();
            //ExistStockMatching();


            #endregion

            #region Stocks

            //NewStock();
            //GetStock();
            //GetStocks();
            //UpdateStock();
            //DeleteStock();
            //ExistStock();


            #endregion

            #region QuotationRules

            //NewQuotationRule();
            //GetQuotationRule();
            GetQuotationRules();
            //UpdateQuotationRule();
            //DeleteQuotationRule();
            //ExistQuotationRule();


            #endregion


        }



        #region Brand

        static void NewBrand()
        {
            Brand brand = BrandSpecs.New();

            brand.Code = "CMM";
            brand.Description = "XXX XXXX";


            try
            {
                engine.Brands.Store(brand);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetBrand()
        {
            try
            {
                Brand brand = engine.Brands.Get("ASUS");

                Console.WriteLine(BrandSpecs.Describe(brand));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            
        }

        static void GetBrands() 
        {
            try
            {
                List<Brand> brands = new List<Brand>();
                brands = engine.Brands.Get(DataState.All);

                foreach (Brand b in brands)
                {
                    Console.WriteLine(BrandSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateBrand() 
        {
            try
            {
                Brand brand = engine.Brands.Get("ASUS");

                Console.WriteLine(BrandSpecs.Describe(brand));
                Console.ReadKey();

                brand.Description = "DDDDD DDDDDDD";

                engine.Brands.Store(brand);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteBrand() 
        {
            try
            {
                Brand brand = engine.Brands.Get("ASUS");

                Console.WriteLine(BrandSpecs.Describe(brand));
                Console.ReadKey();

                engine.Brands.Delete(brand);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistBrand() 
        {
            string code = "ASUS";
            string msg = "";

            try
            {
                bool res = engine.Brands.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            


        }

        #endregion


        #region User

        static void NewUser()
        {
            User user = UserSpecs.New();

            user.Username = "huguish";
            user.Password = "1ddfgdfgdg$";
            user.Name = "Marta";
            user.Email = "marta@gmail.com";
            user.Mobile = "1234666";
            user.Sms = "12345";
            user.Administrator = true;


            try
            {
                engine.Users.Store(user);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetUser()
        {
            string username = "huguish";
            string password = "1ddfgdfgdg$";

            bool exist = ExistUser(username, password);
            if (exist)
            {
                try
                {

                    User user = engine.Users.Get("huguish");
                    user.Password = password;

                    Console.WriteLine(UserSpecs.Describe(user));
                    Console.ReadKey();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Username ou password inválidos");
                Console.ReadKey();
            }

           

        }

        static void GetUsers()
        {
            try
            {
                List<User> users = new List<User>();
                users = engine.Users.Get(DataState.All);

                foreach (User b in users)
                {
                    Console.WriteLine(UserSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateUser()
        {
            try
            {
                User user = engine.Users.Get("huguish");
                user.Password = "1ddfgdfgdg#";

                Console.WriteLine(UserSpecs.Describe(user));
                Console.ReadKey();

                user.Name = "Hugo Leite";

                engine.Users.Store(user);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteUser()
        {
            try
            {
                User user = engine.Users.Get("huguish");
                user.Password = "1ddfgdfgdg#";

                Console.WriteLine(UserSpecs.Describe(user));
                Console.ReadKey();

                engine.Users.Delete(user);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static bool ExistUser(string username, string password)
        {
           
            string msg = "";

            try
            {
                bool res = engine.Users.Exists(username, password, DataState.Active, ref msg);
                return res;
                //if (res)
                //{
                //    Console.WriteLine("Existe");
                //    Console.ReadKey();
                //}
                //else
                //{
                //    Console.WriteLine(msg);
                //    Console.ReadKey();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return false;
            }
           



        }

        #endregion


        #region WarningType

        static void NewWarningType()
        {
            WarningType warningType = WarningTypeSpecs.New();

            warningType.Code = "CCC";
            warningType.Description = "ZZZ ZZZZ";
            warningType.Severity = 3;

            try
            {
                engine.WarningTypes.Store(warningType);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void GetWarningType()
        {
            try
            {
                WarningType WarningType = engine.WarningTypes.Get("AAA");

                Console.WriteLine(WarningTypeSpecs.Describe(WarningType));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }



        static void GetWarningTypes()
        {
            try
            {
                List<WarningType> warningTypes = new List<WarningType>();
                warningTypes = engine.WarningTypes.Get(DataState.All);

                foreach (WarningType b in warningTypes)
                {
                    
                    Console.WriteLine(WarningTypeSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateWarningType()
        {
            try
            {
                WarningType warningType = engine.WarningTypes.Get("AAA");

                Console.WriteLine(WarningTypeSpecs.Describe(warningType));
                Console.ReadKey();

                warningType.Notes = "Isto é um teste";

                engine.WarningTypes.Store(warningType);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteWarningType()
        {
            try
            {
                WarningType warningType = engine.WarningTypes.Get("CCC");

                Console.WriteLine(WarningTypeSpecs.Describe(warningType));
                Console.ReadKey();

                engine.WarningTypes.Delete(warningType);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }



        static void ExistWarningType()
        {
            string code = "CCC";
            string msg = "";

            try
            {
                bool res = engine.WarningTypes.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }



        #endregion


        #region Idiomas

        static void SetLanguage() 
        {
            //t = res_man.GetString("MainForm_text", cul);

        }

        static void NewLanguage()
        {

            Language language = LanguageSpecs.New();

            //language.Code = "PT-PT";
            //language.Description = "Portuguese";

            //language.Code = "EN-UK";
            //language.Description = "English";

            language.Code = "EN-US";
            language.Description = "English United States";



            try
            {
                engine.Languages.Store(language);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void GetLanguage()
        {
            try
            {
                Language language = engine.Languages.Get("PT-PT");

                Console.WriteLine(LanguageSpecs.Describe(language));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }


        static void GetLanguages()
        {
            try
            {
                List<Language> languages = new List<Language>();
                languages = engine.Languages.Get(DataState.All);

                foreach (Language b in languages)
                {
                    Console.WriteLine(LanguageSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateLanguage()
        {
            try
            {
                Language language = engine.Languages.Get("PT-PT");

                Console.WriteLine(LanguageSpecs.Describe(language));
                Console.ReadKey();

                language.Description = "Portuguese Portugal";

                engine.Languages.Store(language);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteLanguage()
        {
            try
            {
                Language language = engine.Languages.Get("EN-US");

                Console.WriteLine(LanguageSpecs.Describe(language));
                Console.ReadKey();

                engine.Languages.Delete(language);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void ExistLanguage()
        {
            string code = "EN-US";
            string msg = "";

            try
            {
                bool res = engine.Languages.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion


        #region Suppliers

        static void NewSupplier()
        {
            Supplier supplier = SupplierSpecs.New();

            supplier.Code = "00026";
            supplier.Name = "Databox - Informática de Computadores..";
            supplier.Address = "Rua Monte dos Pipos, 649";
            supplier.ZipCode = "4460-059";
            supplier.City = "Guifões";
            supplier.IdentificationNumber = "999999990";
            supplier.Salesman = "Susana";
            supplier.Phone = "222333444";
            supplier.Cellphone = "939949959";
            supplier.SMS = "969979989";
            supplier.Email = "susana@cpcdi.pt";
            supplier.ActiveOnlineAccess = false;
            supplier.Username = "cpc001";
            supplier.Password = "cpcpass1#";
            supplier.SuggestionExpirationHours = 24;
            supplier.AutomaticProductMatching = true;
            supplier.ActomaticProductCreation = true;
            supplier.InfoProductDetailAvailable = true;
            supplier.InicialScoreDescription = 5;
            supplier.InicialScoreFeatures = 5;
            supplier.InicialScoreLink = 5;
            supplier.InicialScoreImage = 5;
            supplier.InactiveDescriptionSuggestion = false;
            supplier.InactiveFeatureSuggestion = false;
            supplier.InactiveLinkSuggestion = false;
            supplier.InactiveImageSuggestion = false;
            supplier.InactiveAutomaticUpdateSuggestion = false;
            supplier.ProductPriceTrust = 90;
            supplier.ProductAvailableTrust = 90;




            try
            {
                engine.Suppliers.Store(supplier);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void GetSupplier()
        {
            try
            {
                Supplier supplier = engine.Suppliers.Get("00025");

                Console.WriteLine(SupplierSpecs.Describe(supplier));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }


        static void GetSuppliers()
        {
            try
            {
                List<Supplier> supplier = new List<Supplier>();
                supplier = engine.Suppliers.Get(DataState.All);

                foreach (Supplier b in supplier)
                {
                    Console.WriteLine(SupplierSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateSupplier()
        {
            try
            {
                Supplier supplier = engine.Suppliers.Get("00025");

                Console.WriteLine(SupplierSpecs.Describe(supplier));
                Console.ReadKey();

                supplier.City = "Guimarães";

                engine.Suppliers.Store(supplier);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteSupplier()
        {
            try
            {
                Supplier supplier = engine.Suppliers.Get("00026");

                Console.WriteLine(SupplierSpecs.Describe(supplier));
                Console.ReadKey();

                engine.Suppliers.Delete(supplier);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void ExistSupplier()
        {
            string code = "00025";
            string msg = "";

            try
            {
                bool res = engine.Suppliers.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion


        #region Categories

        static void NewCategory()
        {
            Category category = CategorySpecs.New();

            category.Code = "Comp";
            category.Description = "Computedores";
            category.UnityWeightAverage = 1000;
            category.MinPriceAllowed = 350;
            category.MaxPriceAllowed  = 4000;
            category.MaxPriceAmplitude = 200;
            category.Trust=0.7;

           



            try
            {
                engine.Categories.Store(category);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void GetCategory()
        {
            try
            {
                Category category = engine.Categories.Get("Comp");

                Console.WriteLine(CategorySpecs.Describe(category));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }


        static void GetCategories()
        {
            try
            {
                List<Category> category = new List<Category>();
                category = engine.Categories.Get(DataState.All);

                foreach (Category b in category)
                {
                    Console.WriteLine(CategorySpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateCategory()
        {
            try
            {
                Category category = engine.Categories.Get("Comp");

                Console.WriteLine(CategorySpecs.Describe(category));
                Console.ReadKey();

                category.Description = "Computadores";
                category.UnityWeightAverage = 1000;
                category.MinPriceAllowed = 350;
                category.MaxPriceAllowed = 4000;
                category.MaxPriceAmplitude = 200;
                category.Trust = 0.7;

                engine.Categories.Store(category);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteCategory()
        {
            try
            {
                Category category = engine.Categories.Get("COMP");

                Console.WriteLine(CategorySpecs.Describe(category));
                Console.ReadKey();

                engine.Categories.Delete(category);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void ExistCategory()
        {
            string code = "IMP";
            string msg = "";

            try
            {
                bool res = engine.Categories.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion


        #region Supplements

        static void NewSupplement()
        {
            Supplement supplement = SupplementSpecs.New();

            supplement.Code = "PRM";
            supplement.Description = "Promoção";
            supplement.TextToAdd = "PROM";
            supplement.TextToRemove= "C";
          
            try
            {
                engine.Supplements.Store(supplement);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void GetSupplement()
        {
            try
            {
                Supplement supplement = engine.Supplements.Get("N/A");

                Console.WriteLine(SupplementSpecs.Describe(supplement));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }


        static void GetSupplements()
        {
            try
            {
                List<Supplement> supplements = new List<Supplement>();
                supplements = engine.Supplements.Get(DataState.All);

                foreach (Supplement b in supplements)
                {
                    Console.WriteLine(SupplementSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateSupplement()
        {
            try
            {
                Supplement supplement = engine.Supplements.Get("PRM");

                Console.WriteLine(SupplementSpecs.Describe(supplement));
                Console.ReadKey();

             
                supplement.Description = "Promocao";
                supplement.TextToAdd = "";
                supplement.TextToRemove = "";


                engine.Supplements.Store(supplement);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteSupplement()
        {
            try
            {
                Supplement supplement = engine.Supplements.Get("PRM");

                Console.WriteLine(SupplementSpecs.Describe(supplement));
                Console.ReadKey();

                engine.Supplements.Delete(supplement);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void ExistSupplement()
        {
            string code = "PRM";
            string msg = "";

            try
            {
                bool res = engine.Supplements.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion


        #region State

        static void NewState()
        {
            State state = StateSpecs.New();

            state.Code = "Teste";
            state.Description = "XXX XXXX";


            try
            {
                engine.States.Store(state);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void GetState()
        {
            try
            {
                State state = engine.States.Get("Teste");

                Console.WriteLine(StateSpecs.Describe(state));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }


        static void GetStates()
        {
            try
            {
                List<State> states = new List<State>();
                states = engine.States.Get(DataState.All);

                foreach (State b in states)
                {
                    Console.WriteLine(StateSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateState()
        {
            try
            {
                State state = engine.States.Get("Teste");

                Console.WriteLine(StateSpecs.Describe(state));
                Console.ReadKey();

                state.Description = "DDDDD DDDDDDD";

                engine.States.Store(state);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteState()
        {
            try
            {
                State state = engine.States.Get("Teste");

                Console.WriteLine(StateSpecs.Describe(state));
                Console.ReadKey();

                engine.States.Delete(state);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void ExistState()
        {
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.States.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region Tax

        static void NewTax()
        {
            Tax tax = TaxSpecs.New();

            tax.Code = "Teste";
            tax.Description = "XXX XXXX";
            tax.TaxDesignation = "Imposto extra";
            tax.TaxValue = 3;


            try
            {
                engine.Taxes.Store(tax);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void GetTax()
        {
            try
            {
                Tax tax = engine.Taxes.Get("Teste");

                Console.WriteLine(TaxSpecs.Describe(tax));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }


        static void GetTaxes()
        {
            try
            {
                List<Tax> taxes = new List<Tax>();
                taxes = engine.Taxes.Get(DataState.All);

                foreach (Tax b in taxes)
                {
                    Console.WriteLine(TaxSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateTax()
        {
            try
            {
                Tax tax = engine.Taxes.Get("Teste");

                Console.WriteLine(TaxSpecs.Describe(tax));
                Console.ReadKey();

                tax.Description = "DDDDD DDDDDDD";

                engine.Taxes.Store(tax);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteTax()
        {
            try
            {
                Tax tax = engine.Taxes.Get("Teste");

                Console.WriteLine(TaxSpecs.Describe(tax));
                Console.ReadKey();

                engine.Taxes.Delete(tax);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void ExistTax()
        {
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.Taxes.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region WorryingTerms

        static void NewWorryingTerm()
        {
            WorryingTerm worryingTerm = WorryingTermSpecs.New();

            worryingTerm.Term = "Teste";
            worryingTerm.Index = 3;
            worryingTerm.Notes = "XXX XXXX";
           


            try
            {
                engine.WorryingTerms.Store(worryingTerm);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void GetWorryingTerm()
        {
            try
            {
                WorryingTerm worryingTerm = engine.WorryingTerms.Get("Teste");

                Console.WriteLine(WorryingTermSpecs.Describe(worryingTerm));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }


        static void GetWorryingTerms()
        {
            try
            {
                List<WorryingTerm> worryingTerms = new List<WorryingTerm>();
                worryingTerms = engine.WorryingTerms.Get(DataState.All);

                foreach (WorryingTerm b in worryingTerms)
                {
                    Console.WriteLine(WorryingTermSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateWorryingTerm()
        {
            try
            {
                WorryingTerm worryingTerm = engine.WorryingTerms.Get("Teste");

                Console.WriteLine(WorryingTermSpecs.Describe(worryingTerm));
                Console.ReadKey();

                worryingTerm.Notes = "DDDDD DDDDDDD";

                engine.WorryingTerms.Store(worryingTerm);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteWorryingTerm()
        {
            try
            {
                WorryingTerm worryingTerm = engine.WorryingTerms.Get("Teste");

                Console.WriteLine(WorryingTermSpecs.Describe(worryingTerm));
                Console.ReadKey();

                engine.WorryingTerms.Delete(worryingTerm);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void ExistWorryingTerm()
        {
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.WorryingTerms.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region Group

        static void NewGroup()
        {
            Group group = GroupSpecs.New();

            group.Code = "Teste";
            group.Description = "XXX XXXX";


            try
            {
                engine.Groups.Store(group);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetGroup()
        {
            try
            {
                Group group = engine.Groups.Get("Teste");

                Console.WriteLine(GroupSpecs.Describe(group));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetGroups()
        {
            try
            {
                List<Group> groups = new List<Group>();
                groups = engine.Groups.Get(DataState.All);

                foreach (Group b in groups)
                {
                    Console.WriteLine(GroupSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateGroup()
        {
            try
            {
                Group group = engine.Groups.Get("Teste");

                Console.WriteLine(GroupSpecs.Describe(group));
                Console.ReadKey();

                group.Description = "DDDDD DDDDDDD";

                engine.Groups.Store(group);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteGroup()
        {
            try
            {
                Group group = engine.Groups.Get("Teste");

                Console.WriteLine(GroupSpecs.Describe(group));
                Console.ReadKey();

                engine.Groups.Delete(group);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistGroup()
        {
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.Groups.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region Catalog

        static void NewCatalog()
        {
            Catalog catalog = CatalogSpecs.New();

            catalog.Code = "Teste";
            catalog.Description = "XXX XXXX";


            try
            {
                engine.Catalogs.Store(catalog);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetCatalog()
        {
            try
            {
                Catalog catalog = engine.Catalogs.Get("Teste");

                Console.WriteLine(CatalogSpecs.Describe(catalog));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetCatalogs()
        {
            try
            {
                List<Catalog> catalogs = new List<Catalog>();
                catalogs = engine.Catalogs.Get(DataState.All);

                foreach (Catalog b in catalogs)
                {
                    Console.WriteLine(CatalogSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateCatalog()
        {
            try
            {
                Catalog catalog = engine.Catalogs.Get("Teste");

                Console.WriteLine(CatalogSpecs.Describe(catalog));
                Console.ReadKey();

                catalog.Description = "DDDDD DDDDDDD";

                engine.Catalogs.Store(catalog);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteCatalog()
        {
            try
            {
                Catalog catalog = engine.Catalogs.Get("Teste");

                Console.WriteLine(CatalogSpecs.Describe(catalog));
                Console.ReadKey();

                engine.Catalogs.Delete(catalog);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistCatalog()
        {
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.Catalogs.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region Classes

        static void NewClasse()
        {
            Classe classe = ClasseSpecs.New();

            classe.Code = "Test2";
            classe.Description = "YYYVHVJHBJBKJJHGJ";
            classe.Catalog = engine.Catalogs.Get("Teste");
            classe.Range = 20;
            classe.RangeMinValue= 50;
           
           


            try
            {
                engine.Classes.Store(classe);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetClasse()
        {
            try
            {
                Classe classe = engine.Classes.Get("Teste", 1);

                Console.WriteLine(ClasseSpecs.Describe(classe));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetClasses()
        {
            try
            {
                List<Classe> classes = new List<Classe>();
                classes = engine.Classes.Get(DataState.All, 1);

                foreach (Classe b in classes)
                {
                    Console.WriteLine(ClasseSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateClasse()
        {
            try
            {
                Classe classe = engine.Classes.Get("Teste", 0);

                Console.WriteLine(ClasseSpecs.Describe(classe));
                Console.ReadKey();

                classe.Description = "123eeee";

                engine.Classes.Store(classe);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteClasse()
        {
            try
            {
                Classe classe = engine.Classes.Get("Teste", 0);

                Console.WriteLine(ClasseSpecs.Describe(classe));
                Console.ReadKey();

                engine.Classes.Delete(classe);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistClasse()
        {
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.Classes.Exists(code, DataState.Active, ref msg);

                
                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region BrandMatching

        static void NewBrandMatching()
        {
            BrandMatching brandMatching = BrandMatchingSpecs.New();

            
            brandMatching.Code = "Test2";
            brandMatching.Description = "YYYVHVJHBJBKJJHGJ";
            //brandMatching.MapTo = engine.Brands.Get("01HP");
           

            try
            {
                brandMatching.Supplier = engine.Suppliers.Get("00025");
                brandMatching.MapTo = engine.Brands.Get("01HP");
                engine.BrandsMatching.Store(brandMatching);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetBrandMatching()
        {
            try
            {
                BrandMatching brandMatching = engine.BrandsMatching.Get("00024", "Teste", 1, 1);

                Console.WriteLine(BrandMatchingSpecs.Describe(brandMatching));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetBrandsMatching()
        {
            try
            {
                List<BrandMatching> brandsMatching = new List<BrandMatching>();
                brandsMatching = engine.BrandsMatching.Get(DataState.All, 1, 1);

                foreach (BrandMatching b in brandsMatching)
                {
                    Console.WriteLine(BrandMatchingSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateBrandMatching()
        {
            try
            {
                BrandMatching brandMatching = engine.BrandsMatching.Get("00024", "Teste", 0, 0);

                brandMatching.MapTo = engine.Brands.Get("01HP");

                Console.WriteLine(BrandMatchingSpecs.Describe(brandMatching));
                Console.ReadKey();

                brandMatching.Description = "123eeee";

                engine.BrandsMatching.Store(brandMatching);

                Console.WriteLine("Actualizado com sucesso!");

                Console.WriteLine(BrandMatchingSpecs.Describe(brandMatching));
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteBrandMatching()
        {
            try
            {
                BrandMatching brandMatching = engine.BrandsMatching.Get("00024", "Teste", 0, 0);

                Console.WriteLine(BrandMatchingSpecs.Describe(brandMatching));
                Console.ReadKey();

                engine.BrandsMatching.Delete(brandMatching);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistBrandMatching()
        {
        
            string supplierCode = "00024";
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.BrandsMatching.Exists(supplierCode, code, DataState.Active, ref msg);


                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region CategoryMatching

        static void NewCategoryMatching()
        {
            CategoryMatching categoryMatching = CategoryMatchingSpecs.New();


            categoryMatching.Code = "Test2";
            categoryMatching.Description = "YYYVHVJHBJBKJJHGJ";
            //brandMatching.MapTo = engine.Brands.Get("01HP");


            try
            {
                categoryMatching.Supplier = engine.Suppliers.Get("00024");
                categoryMatching.MapTo = engine.Categories.Get("COMP");
                engine.CategoriesMatching.Store(categoryMatching);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetCategoryMatching()
        {
            try
            {
                CategoryMatching categoryMatching = engine.CategoriesMatching.Get("00025", "Teste", 1, 1);

                Console.WriteLine(CategoryMatchingSpecs.Describe(categoryMatching));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetCategoriesMatching()
        {
            try
            {
                List<CategoryMatching> categoriesMatching = new List<CategoryMatching>();
                categoriesMatching = engine.CategoriesMatching.Get(DataState.All, 1, 1);

                foreach (CategoryMatching b in categoriesMatching)
                {
                    Console.WriteLine(CategoryMatchingSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateCategoryMatching()
        {
            try
            {
                CategoryMatching categoryMatching = engine.CategoriesMatching.Get("00025", "Teste", 0, 0);

                categoryMatching.MapTo = engine.Categories.Get("IMP");

                Console.WriteLine(CategoryMatchingSpecs.Describe(categoryMatching));
                Console.ReadKey();

                categoryMatching.Description = "123eeee";

                engine.CategoriesMatching.Store(categoryMatching);

                Console.WriteLine("Actualizado com sucesso!");

                Console.WriteLine(CategoryMatchingSpecs.Describe(categoryMatching));
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteCategoryMatching()
        {
            try
            {

                CategoryMatching categoryMatching = engine.CategoriesMatching.Get("00025", "Teste", 1,  1);

                Console.WriteLine(CategoryMatchingSpecs.Describe(categoryMatching));
                Console.ReadKey();

                engine.CategoriesMatching.Delete(categoryMatching);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistCategoryMatching()
        {

            string supplierCode = "00025";
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.CategoriesMatching.Exists(supplierCode, code, DataState.Active, ref msg);


                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region StateMatching

        static void NewStateMatching()
        {
            StateMatching stateMatching = StateMatchingSpecs.New();


            stateMatching.Code = "Teste";
            stateMatching.Description = "YYYVHVJHBJBKJJHGJ";
         

            try
            {
                stateMatching.Supplier = engine.Suppliers.Get("00025");
                stateMatching.MapTo = engine.States.Get("PROMO");
                engine.StatesMatching.Store(stateMatching);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetStateMatching()
        {
            try
            {
                StateMatching stateMatching = engine.StatesMatching.Get("00024", "Teste", 1, 1);

                Console.WriteLine(StateMatchingSpecs.Describe(stateMatching));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetStatesMatching()
        {
            try
            {
                List<StateMatching> statesMatching = new List<StateMatching>();
                statesMatching = engine.StatesMatching.Get(DataState.All, 1, 1);

                foreach (StateMatching b in statesMatching)
                {
                    Console.WriteLine(StateMatchingSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateStateMatching()
        {
            try
            {
                StateMatching stateMatching = engine.StatesMatching.Get("00024", "Teste", 0, 0);

                stateMatching.MapTo = engine.States.Get("NOVO");

                Console.WriteLine(StateMatchingSpecs.Describe(stateMatching));
                Console.ReadKey();

                stateMatching.Description = "123eeee";

                engine.StatesMatching.Store(stateMatching);

                Console.WriteLine("Actualizado com sucesso!");

                Console.WriteLine(StateMatchingSpecs.Describe(stateMatching));
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteStateMatching()
        {
            try
            {

                StateMatching stateMatching = engine.StatesMatching.Get("00024", "Teste", 1, 1);

                Console.WriteLine(StateMatchingSpecs.Describe(stateMatching));
                Console.ReadKey();

                engine.StatesMatching.Delete(stateMatching);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistStateMatching()
        {

            string supplierCode = "00024";
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.StatesMatching.Exists(supplierCode, code, DataState.Active, ref msg);


                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region TaxMatching

        static void NewTaxMatching()
        {
            TaxMatching taxMatching = TaxMatchingSpecs.New();


            taxMatching.Code = "Test2";
            taxMatching.Description = "YYYVHVJHBJBKJJHGJ";
            //brandMatching.MapTo = engine.Brands.Get("01HP");


            try
            {
                taxMatching.Supplier = engine.Suppliers.Get("00024");
                taxMatching.MapTo = engine.Taxes.Get("PT.23");
                engine.TaxesMatching.Store(taxMatching);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetTaxMatching()
        {
            try
            {
                TaxMatching taxMatching = engine.TaxesMatching.Get("00025", "Teste", 1, 1);

                Console.WriteLine(TaxMatchingSpecs.Describe(taxMatching));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetTaxesMatching()
        {
            try
            {
                List<TaxMatching> taxesMatching = new List<TaxMatching>();
                taxesMatching = engine.TaxesMatching.Get(DataState.All, 1, 1);

                foreach (TaxMatching b in taxesMatching)
                {
                    Console.WriteLine(TaxMatchingSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateTaxMatching()
        {
            try
            {
                TaxMatching taxMatching = engine.TaxesMatching.Get("00025", "Teste", 0, 0);

                taxMatching.MapTo = engine.Taxes.Get("PT.23");

                Console.WriteLine(TaxMatchingSpecs.Describe(taxMatching));
                Console.ReadKey();

                taxMatching.Description = "123eeee";

                engine.TaxesMatching.Store(taxMatching);

                Console.WriteLine("Actualizado com sucesso!");

                Console.WriteLine(TaxMatchingSpecs.Describe(taxMatching));
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteTaxMatching()
        {
            try
            {

                TaxMatching taxMatching = engine.TaxesMatching.Get("00025", "Teste", 1, 1);

                Console.WriteLine(TaxMatchingSpecs.Describe(taxMatching));
                Console.ReadKey();

                engine.TaxesMatching.Delete(taxMatching);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistTaxMatching()
        {

            string supplierCode = "00025";
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.TaxesMatching.Exists(supplierCode, code, DataState.Active, ref msg);


                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region SupplementMatching

        static void NewSupplementMatching()
        {
            SupplementMatching supplementMatching = SupplementMatchingSpecs.New();


            supplementMatching.Code = "Test2";
            supplementMatching.Description = "YYYVHVJHBJBKJJHGJ";
            //brandMatching.MapTo = engine.Brands.Get("01HP");


            try
            {
                supplementMatching.Supplier = engine.Suppliers.Get("00024");
                supplementMatching.MapTo = engine.Supplements.Get("N/A");
                engine.SupplementsMatching.Store(supplementMatching);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetSupplementMatching()
        {
            try
            {
                SupplementMatching supplementMatching = engine.SupplementsMatching.Get("00025", "Teste", 1, 1);

                Console.WriteLine(SupplementMatchingSpecs.Describe(supplementMatching));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetSupplementsMatching()
        {
            try
            {
                List<SupplementMatching> supplementMatching = new List<SupplementMatching>();
                supplementMatching = engine.SupplementsMatching.Get(DataState.All, 1, 1);

                foreach (SupplementMatching b in supplementMatching)
                {
                    Console.WriteLine(SupplementMatchingSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateSupplementMatching()
        {
            try
            {
                SupplementMatching supplementMatching = engine.SupplementsMatching.Get("00025", "Teste", 0, 0);

                supplementMatching.MapTo = engine.Supplements.Get("N/A");

                Console.WriteLine(SupplementMatchingSpecs.Describe(supplementMatching));
                Console.ReadKey();

                supplementMatching.Description = "123eeee";

                engine.SupplementsMatching.Store(supplementMatching);

                Console.WriteLine("Actualizado com sucesso!");

                Console.WriteLine(SupplementMatchingSpecs.Describe(supplementMatching));
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteSupplementMatching()
        {
            try
            {

                SupplementMatching supplementMatching = engine.SupplementsMatching.Get("00025", "Teste", 1, 1);

                Console.WriteLine(SupplementMatchingSpecs.Describe(supplementMatching));
                Console.ReadKey();

                engine.SupplementsMatching.Delete(supplementMatching);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistSupplementMatching()
        {

            string supplierCode = "00025";
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.SupplementsMatching.Exists(supplierCode, code, DataState.Active, ref msg);


                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region StockMatching

        static void NewStockMatching()
        {
            StockMatching stockMatching = StockMatchingSpecs.New();


            stockMatching.Code = "Test2";
            stockMatching.Description = "YYYVHVJHBJBKJJHGJ";
            //brandMatching.MapTo = engine.Brands.Get("D+0");


            try
            {
                stockMatching.Supplier = engine.Suppliers.Get("00025");
                stockMatching.MapTo = engine.Stocks.Get("D+1");
                engine.StocksMatching.Store(stockMatching);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetStockMatching()
        {
            try
            {
                StockMatching stockMatching = engine.StocksMatching.Get("00025", "Teste", 1, 1);

                Console.WriteLine(StockMatchingSpecs.Describe(stockMatching));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetStocksMatching()
        {
            try
            {
                List<StockMatching> stocksMatching = new List<StockMatching>();
                stocksMatching = engine.StocksMatching.Get(DataState.All, 1, 1);

                foreach (StockMatching b in stocksMatching)
                {
                    Console.WriteLine(StockMatchingSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateStockMatching()
        {
            try
            {
                StockMatching stockMatching = engine.StocksMatching.Get("00025", "Teste", 0, 0);

                //stockMatching.MapTo = engine.Stocks.Get("IMP");

                Console.WriteLine(StockMatchingSpecs.Describe(stockMatching));
                Console.ReadKey();

                stockMatching.Description = "123eeee";
                stockMatching.MapTo = engine.Stocks.Get("D+1");

                engine.StocksMatching.Store(stockMatching);

                Console.WriteLine("Actualizado com sucesso!");

                Console.WriteLine(StockMatchingSpecs.Describe(stockMatching));
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteStockMatching()
        {
            try
            {

                StockMatching stockMatching = engine.StocksMatching.Get("00025", "Teste", 1, 1);

                Console.WriteLine(StockMatchingSpecs.Describe(stockMatching));
                Console.ReadKey();

                engine.StocksMatching.Delete(stockMatching);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistStockMatching()
        {

            string supplierCode = "00025";
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.StocksMatching.Exists(supplierCode, code, DataState.Active, ref msg);


                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion


        #region SupplierBrand

        static void NewSupplierBrand()
        {
            SupplierBrand supplierBrand = SupplierBrandSpecs.New();


            supplierBrand.Trust = 0.8;
            supplierBrand.Notes = "YYYVHVJHBJBKJJHGJ";
           


            try
            {
                supplierBrand.Supplier = engine.Suppliers.Get("00025");
                supplierBrand.Brand = engine.Brands.Get("MAC");
                engine.SuppliersBrands.Store(supplierBrand);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetSupplierBrand()
        {
            try
            {
            
                Supplier supplier = engine.Suppliers.Get("00025");
                Brand brand= engine.Brands.Get("MAC");
                SupplierBrand supplierBrand = engine.SuppliersBrands.Get(supplier, brand);

                Console.WriteLine(SupplierBrandSpecs.Describe(supplierBrand));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetSuppliersBrands()
        {
            try
            {
                List<SupplierBrand> supplierBrand = new List<SupplierBrand>();
                //supplierBrand = engine.SuppliersBrands.Get(1, 1);
                Supplier supplier = engine.Suppliers.Get("00025");
                supplierBrand = engine.SuppliersBrands.Get(supplier, 1);

                foreach (SupplierBrand b in supplierBrand)
                {
                    Console.WriteLine(SupplierBrandSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateSupplierBrand()
        {
            try
            {
                SupplierBrand supplierBrand = engine.SuppliersBrands.Get("00025", "MAC", 0, 0);

                //stockMatching.MapTo = engine.Stocks.Get("IMP");

                Console.WriteLine(SupplierBrandSpecs.Describe(supplierBrand));
                Console.ReadKey();

                supplierBrand.Notes = "123eeee";
                supplierBrand.Trust = 1;

                engine.SuppliersBrands.Store(supplierBrand);

                Console.WriteLine("Actualizado com sucesso!");

                Console.WriteLine(SupplierBrandSpecs.Describe(supplierBrand));
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteSupplierBrand()
        {
            try
            {

                SupplierBrand supplierBrand = engine.SuppliersBrands.Get("00025", "MAC", 0, 0);

                Console.WriteLine(SupplierBrandSpecs.Describe(supplierBrand));
                Console.ReadKey();

                engine.SuppliersBrands.Delete(supplierBrand);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistSupplierBrand()
        {

            string supplierCode = "00025";
            string brandCode = "Mac";
            string msg = "";

            try
            {
                bool res = engine.SuppliersBrands.Exists(supplierCode, brandCode, ref msg);


                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region Stock

        static void NewStock()
        {
            Stock stock = StockSpecs.New();

            stock.Code = "Teste";
            stock.Description = "XXX XXXX";
            stock.AvailabilityLevel = 5;



            try
            {
                engine.Stocks.Store(stock);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetStock()
        {
            try
            {
                Stock stock = engine.Stocks.Get("teste", DataState.Active, 1, 1, 1, 1, 1 );

                Console.WriteLine(StockSpecs.Describe(stock));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetStocks()
        {
            try
            {
                List<Stock> stocks = new List<Stock>();
                stocks = engine.Stocks.Get(DataState.All, 1, 1, 1, 1, 1);

                foreach (Stock b in stocks)
                {
                    Console.WriteLine(StockSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void UpdateStock()
        {
            try
            {
                Stock stock = engine.Stocks.Get("teste");

                Console.WriteLine(StockSpecs.Describe(stock));
                Console.ReadKey();

                stock.Description = "DDDDD DDDDDDD";
                stock.Notes = "isto é um teste";

                stock.StockCodeExpirationP50 = engine.Stocks.Get("D+5");
          

                engine.Stocks.Store(stock);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DeleteStock()
        {
            try
            {
                Stock stock = engine.Stocks.Get("teste");

                Console.WriteLine(StockSpecs.Describe(stock));
                Console.ReadKey();

                engine.Stocks.Delete(stock);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ExistStock()
        {
            string code = "Teste";
            string msg = "";

            try
            {
                bool res = engine.Stocks.Exists(code, DataState.Active, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion

        #region QuotationRule

        static void NewQuotationRule()
        {
            QuotationRule quotationRule = QuotationRuleSpecs.New();

            
            quotationRule.ExpitationHours = 24;
            quotationRule.DataReset = DateTime.Now.AddDays(10);



            try
            {
                quotationRule.Supplier = engine.Suppliers.Get("00025");
                quotationRule.Brand = engine.Brands.Get("01HP");
                quotationRule.Category = engine.Categories.Get("IMP");
                quotationRule.Stock = engine.Stocks.Get("D+0");

                engine.QuotationRules.Store(quotationRule);

                Console.WriteLine("Inserido com sucesso");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void GetQuotationRule()
        {
            try
            {
                Supplier supplier= engine.Suppliers.Get("00024");
                Brand brand= engine.Brands.Get("01HP");
                Category category= engine.Categories.Get("IMP");
                Stock stock= engine.Stocks.Get("D+0");
                QuotationRule quotationRule = engine.QuotationRules.Get(supplier, brand, category, stock, true, false, 1);

                Console.WriteLine(QuotationRuleSpecs.Describe(quotationRule));
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void GetQuotationRules()
        {
            try
            {
                Supplier supplier = engine.Suppliers.Get("00025");
                Brand brand = engine.Brands.Get("01HP");
                Category category = engine.Categories.Get("IMP");
                Stock stock = engine.Stocks.Get("D+0");

                List<QuotationRule> quotationRules = new List<QuotationRule>();
                quotationRules = engine.QuotationRules.Get(supplier, brand, category, true, true, 0, 0);

                foreach (QuotationRule b in quotationRules)
                {
                    Console.WriteLine(QuotationRuleSpecs.Describe(b));
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void UpdateQuotationRule()
        {
            try
            {
                Supplier supplier = engine.Suppliers.Get("00024");
                Brand brand = engine.Brands.Get("01HP");
                Category category = engine.Categories.Get("IMP");
                Stock stock = engine.Stocks.Get("D+0");

                QuotationRule quotationRule = engine.QuotationRules.Get(supplier, brand, category, stock, false, false, 0);

                Console.WriteLine(QuotationRuleSpecs.Describe(quotationRule));
                Console.ReadKey();

                quotationRule.SubstituteStock = engine.Stocks.Get("D+1");
                quotationRule.DataReset = DateTime.Now.AddDays(3);
                quotationRule.Notes = "isto é um teste";

                engine.QuotationRules.Store(quotationRule);

                Console.WriteLine("Actualizado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void DeleteQuotationRule()
        {
            try
            {
                Supplier supplier = engine.Suppliers.Get("00024");
                Brand brand = engine.Brands.Get("01HP");
                Category category = engine.Categories.Get("IMP");
                Stock stock = engine.Stocks.Get("D+0");
                QuotationRule quotationRule = engine.QuotationRules.Get(supplier, brand, category, stock, false, false, 0);

                Console.WriteLine(QuotationRuleSpecs.Describe(quotationRule));
                Console.ReadKey();

                engine.QuotationRules.Delete(quotationRule);

                Console.WriteLine("Eliminado com sucesso!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        static void ExistQuotationRule()
        {
            //string code = "Teste";
            string msg = "";

            try
            {
                Supplier supplier = engine.Suppliers.Get("00024");
                Brand brand = engine.Brands.Get("01HP");
                Category category = engine.Categories.Get("IMP");
                Stock stock = engine.Stocks.Get("D+0");

                bool res = engine.QuotationRules.Exists(supplier, brand, category, stock, ref msg);

                if (res)
                {
                    Console.WriteLine("Existe");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }



        }

        #endregion


    }
}
