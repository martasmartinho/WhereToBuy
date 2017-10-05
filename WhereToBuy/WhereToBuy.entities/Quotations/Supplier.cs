using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This Classe describes a suplier
    /// </summary>
    [Serializable]
    public class Supplier : BaseEntity
    {

        #region Atributs

        string name;
        string address;
        string zipCode;
        string city;
        string identificationNumber;
        string salesman;
        string phone;
        string cellphone;
        string sms;
        string smail;
        bool activeOnlineAccess;
        string username;
        string password;
        short suggestionExpirationHours;
        bool automaticProductMatching;
        bool actomaticProductCreation;
        bool infoProductDetailAvailable;
        short inicialScoreDescription;
        short inicialScoreFeatures;
        short inicialScoreLink;
        short inicialScoreImage;
        bool inactiveDescriptionSuggestion;
        bool inactiveFeatureSuggestion;
        bool inactiveLinkSuggestion;
        bool inactiveImageSuggestion;
        bool inactiveAutomaticUpdateSuggestion;
        double productPriceTrust;
        double productAvailableTrust;
        //double trustIndex;
      
        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Supplier()
        { }

        
        /// <summary>
        /// Supplier constructor
        /// </summary>
        /// <param name="code">Supplier code</param>
        /// <param name="name">Supplier name</param>
        /// <param name="address">Supplier adress</param>
        /// <param name="zipCode"></param>
        /// <param name="city"></param>
        /// <param name="identificationNumber"></param>
        /// <param name="salesman"></param>
        /// <param name="phone"></param>
        /// <param name="cellphone"></param>
        /// <param name="sms"></param>
        /// <param name="smail"></param>
        /// <param name="activeOnlineAccess"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="suggestionExpirationHours"></param>
        /// <param name="automaticProductMatching"></param>
        /// <param name="actomaticProductCreation"></param>
        /// <param name="infoProductDetailAvailable"></param>
        /// <param name="initialScoreDescription"></param>
        /// <param name="inicialScoreFeatures"></param>
        /// <param name="inicialScoreLink"></param>
        /// <param name="inicialScoreImage"></param>
        /// <param name="inactiveDescriptionSuggestion"></param>
        /// <param name="inactiveFeatureSuggestion"></param>
        /// <param name="inactiveLinkSuggestion"></param>
        /// <param name="inactiveImageSugestion"></param>
        /// <param name="inactiveAutomaticUpdateSuggestion"></param>
        /// <param name="productPriceTrust"></param>
        /// <param name="productAvailableTrust"></param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Supplier(string code, string name, string address, string zipCode, string city, 
                            string identificationNumber, string salesman, string phone, string cellphone, 
                            string sms, string smail, bool activeOnlineAccess, string username, string password,
                            short suggestionExpirationHours, bool automaticProductMatching, bool actomaticProductCreation,
                            bool infoProductDetailAvailable, short initialScoreDescription, short inicialScoreFeatures,
                            short inicialScoreLink, short inicialScoreImage, bool inactiveDescriptionSuggestion, 
                            bool inactiveFeatureSuggestion, bool inactiveLinkSuggestion, bool inactiveImageSugestion, 
                            bool inactiveAutomaticUpdateSuggestion, float productPriceTrust, float productAvailableTrust, 
                            bool inactive, bool editionMode): base(code, inactive, editionMode)
        {

            this.name = name;
            this.address = address;
            this.zipCode = zipCode;
            this.city = city;
            this.identificationNumber = identificationNumber;
            this.salesman = salesman;
            this.phone = phone;
            this.cellphone = cellphone;
            this.sms = sms;
            this.smail = smail;
            this.activeOnlineAccess = activeOnlineAccess;
            this.username = username;
            this.password = password;
            this.suggestionExpirationHours = suggestionExpirationHours;
            this.automaticProductMatching = automaticProductMatching;
            this.actomaticProductCreation = actomaticProductCreation;
            this.infoProductDetailAvailable = infoProductDetailAvailable;
            this.inicialScoreDescription = initialScoreDescription;
            this.inicialScoreFeatures = inicialScoreFeatures;
            this.inicialScoreLink = inicialScoreLink;
            this.inicialScoreImage = inicialScoreImage;
            this.inactiveDescriptionSuggestion = inactiveDescriptionSuggestion;
            this.inactiveFeatureSuggestion = inactiveFeatureSuggestion;
            this.inactiveLinkSuggestion = inactiveLinkSuggestion;
            this.inactiveImageSuggestion = inactiveImageSugestion;
            this.inactiveAutomaticUpdateSuggestion = inactiveAutomaticUpdateSuggestion;
            this.productPriceTrust = productPriceTrust;
            this.productAvailableTrust = productAvailableTrust;


        }

        #endregion

        #region Properties

  
        /// <summary>
        /// 
        /// </summary>
        public string Name 
        { 
            get { return name; } 
            set { name = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string Address 
        {
            get { return address; } 
            set { address = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string ZipCode 
        { 
            get { return zipCode; } 
            set { zipCode = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string City 
        { 
            get { return city; } 
            set { city = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string IdentificationNumber 
        { 
            get { return identificationNumber; } 
            set { identificationNumber = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string Salesman 
        { 
            get { return salesman; } 
            set { salesman = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string Phone 
        { 
            get { return phone; } 
            set { phone = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string Cellphone 
        { 
            get { return cellphone; } 
            set { cellphone = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string SMS 
        { 
            get { return sms; } 
            set { sms = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string Email 
        { 
            get { return smail; } 
            set { smail = value; } 
        }



        public bool ActiveOnlineAccess 
        { 
            get { return activeOnlineAccess; } 
            set { activeOnlineAccess = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string Username 
        { 
            get { return username; } 
            set { username = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public string Password 
        { 
            get { return password; } 
            set { password = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public short SuggestionExpirationHours 
        { 
            get { return suggestionExpirationHours; } 
            set { suggestionExpirationHours = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public bool AutomaticProductMatching 
        { 
            get { return automaticProductMatching; } 
            set { automaticProductMatching = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public bool ActomaticProductCreation 
        { 
            get { return actomaticProductCreation; } 
            set { actomaticProductCreation = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public bool InfoProductDetailAvailable 
        { 
            get { return infoProductDetailAvailable; } 
            set { infoProductDetailAvailable = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public short InicialScoreDescription 
        { 
            get { return inicialScoreDescription; } 
            set { inicialScoreDescription = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public short InicialScoreFeatures 
        { 
            get { return inicialScoreFeatures; } 
            set { inicialScoreFeatures = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public short InicialScoreLink 
        { 
            get { return inicialScoreLink; } 
            set { inicialScoreLink = value; } 
        }


        public short InicialScoreImage 
        { 
            get { return inicialScoreImage; } 
            set { inicialScoreImage = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public bool InactiveDescriptionSuggestion 
        { 
            get { return inactiveDescriptionSuggestion; } 
            set { inactiveDescriptionSuggestion = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public bool InactiveFeatureSuggestion 
        { 
            get { return inactiveFeatureSuggestion; } 
            set { inactiveFeatureSuggestion = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public bool InactiveLinkSuggestion 
        { 
            get { return inactiveLinkSuggestion; } 
            set { inactiveLinkSuggestion = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public bool InactiveImageSuggestion 
        { 
            get { return inactiveImageSuggestion; } 
            set { inactiveImageSuggestion = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public bool InactiveAutomaticUpdateSuggestion 
        { 
            get { return inactiveAutomaticUpdateSuggestion; } 
            set { inactiveAutomaticUpdateSuggestion = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public double ProductPriceTrust 
        { 
            get { return productPriceTrust; } 
            set { productPriceTrust = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        public double ProductAvailableTrust 
        { 
            get { return productAvailableTrust; } 
            set { productAvailableTrust = value; } 
        }


        /// <summary>
        /// 
        /// </summary>
        //public double TrustIndex
        //{
        //    get { return trustIndex; }
        //    set { trustIndex = value; }
        //}

    
        #endregion


        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}] - {1}", base.Code, name);
        }

        /// <summary>
        /// Check if one supplier is equal to another supplier by its code
        /// </summary>
        /// <param name="obj">brand to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((Supplier)obj).Code);
        }

        /// <summary>
        /// Check if two suppliers are equal
        /// </summary>
        /// <param name="supplier1">supplier one</param>
        /// <param name="supplier2">supplier two</param>
        /// <returns></returns>
        public static bool operator ==(Supplier supplier1, Supplier supplier2)
        {
            if ((object)supplier1 == null && (object)supplier2 == null)
            {
                return true;
            }
            else if ((object)supplier1 == null ^ (object)supplier2 == null)
            {
                return false;
            }
            return supplier1.Equals(supplier2);
        }

        /// <summary>
        /// Check if two suppliers are diferent
        /// </summary>
        /// <param name="supplier1">supplier one</param>
        /// <param name="supplier2">supplier two</param>
        /// <returns></returns>
        public static bool operator !=(Supplier supplier1, Supplier supplier2)
        {
            if ((object)supplier1 == null && (object)supplier2 == null)
            {
                return false;
            }
            else if ((object)supplier1 == null ^ (object)supplier2 == null)
            {
                return true;
            }
            return !supplier1.Equals(supplier2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 36;

            if (base.Code == null)
            {
                return hashSignature;
            }
            return hashSignature * base.Code.GetHashCode();
        }


        #endregion
    }
}
