using System;
using System.Configuration;
using WebApplication.Infrastructure.Resources;

namespace WebApplication.Infrastructure
{
    public static class AppSettings
    {
        #region Logging
        public static readonly string APP_LOGS_FILE_PATH = ConfigurationManager
            .AppSettings["LOGGER_FILEPATH"];
        #endregion

        #region ConnectionStrings
        public static readonly string CONNECTION_STRING = ConfigurationManager
            .ConnectionStrings["UberContext"].ConnectionString;
        #endregion

        #region Errors & Warnings
        public static readonly string INTERNAL_SERVER_ERROR_MESSAGE = Errors.INTERNAL_SERVER_ERROR_MESSAGE;
        public static readonly string NOT_FOUND_ERROR_MESSAGE = Errors.NOT_FOUND_ERROR_MESSAGE;
        public static readonly string BAD_REQUEST_ERROR_MESSAGE = Errors.BAD_REQUEST_ERROR_MESSAGE;
        public static readonly string UNAUTHORIZED_ERROR_MESSAGE = Errors.UNAUTHORIZED_ERROR_MESSAGE;
        public static readonly string USER_NOT_FOUND = Errors.USER_NOT_FOUND;
        public static readonly string PATCH_REQUEST_ERROR_MESSAGE = Errors.PATCH_REQUEST_ERROR_MESSAGE;
        public static readonly string PATCH_OPERATION_ERROR_MESSAGE = Errors.PATCH_OPERATION_ERROR_MESSAGE;
        public static readonly string MODELSTATE_ERROR_MESSAGE = Errors.MODELSTATE_ERROR_MESSAGE;
        public static readonly string RECORD_NOT_AVAILABLE = Errors.RECORD_NOT_AVAILABLE;
        #endregion

        #region Paging
        public static readonly int DEFAULT_PAGE_SIZE = int.Parse(ConfigurationManager
            .AppSettings["DEFAULT_PAGE_SIZE"]);
        public static readonly int DEFAULT_PAGES_COUNT = int.Parse(ConfigurationManager
            .AppSettings["DEFAULT_PAGES_COUNT"]);
        #endregion

        #region Images
        public const string DEFAULT_USER_IMAGE_DATA = "iVBORw0KGgoAAAANSUhEUgAAAGUAAABk" +
            "CAYAAACfIP5qAAAACXBIWXMAAAsSAAALEgHS3X78AAAHEklEQVR42u2dTU7jSBTH/x15kSwimgVIsAiZWsAiCxhxADIn6PQJmj5" +
            "Bp08wmRNM+gQTbgA3SEvZIhGkRIJFiXhhS3hhUJDMwhKzcFmExEn8Ua4qx/5LFhKBpOKfX7336uPVp7e3NxRSS1oWGkkpPQFQB+" +
            "D/rLOXzlb82xDAE7tuADwAeCCE9FX/vp9UtBRKaROAf52l8BFDAH3/IoQ8FVCCQbQA+NeW4I+/AnAJ4FIFQFKhUErrANoAziWAW" +
            "AWoRwi5zBUU1j11UuqaeGkCoEMI6W00lIzAkA5HCBTWTXUBfMlwpDoBcC4ieksdCqW0A+DvDUojrgC0CSEPmYPCcosegOMNzO+e" +
            "GZheZqBQStsA/s1B8n3FurQnZaFQSj8z6/iC/GgCoEUIueH1hiXOzryfMyAAcACgz5JfdSyF+Y++QgmgLH3n4WdKBRCu+o9Fm/" +
            "IspQCyVBeEkHPhUFQC4jgOXl9fP/xue3tbdrN+EkK6wqDIBuI4DizLgm3bsG0brusG/l21WsXe3h729/ehaVKmjmL5mMhQWNh7" +
            "w6IOobIsC7quw7btSP+naRoajQZ2dnZkgPkr6tBMHCh9CB5QtCwLd3d3C11UVNVqNRweHsrI/k+iDMtoEYF0RQJxXRfD4TCyZS" +
            "yTrusol8uo1WoioWzBm0A74R4Ss2H3H6K+yXQ6xWAw4AbE1/39PabTqWhrOWYPND8ozI8Im4lzHAfX19dLHTgPMBL0gz3Y3Cy" +
            "lKzLSur29TQ0IANi2LcNaAKDHHvBkUBjdb6JaTSkVcsN0XZcB5QDezGtiS+mKarHrusJulmma3P1VhG6sHhsKpfQcAiepHh8f" +
            "U+22gqxSknqxoLC+ryOypZZlCb0z/oiABJ2tcvqrLKUlOmsXDQUADMOQZS2dOFCEWonjOFLujIwHYZ21lFb4EqFWknQIJUlwI" +
            "Sk8BryVoaEt5Rw5ksjgYk7fgiKxUoCV1JGtFYxcHL5EtcJYShuFRKodBkpLRssUmCmUpQM2aRgMhb14IKt15XIZOX0gWqssp" +
            "SWzZdVqNVcPQ1goTZktk/HElstlVCoV2VCOZ0eP56FIjbpkzKHv7u6q4luaC1DCTsCkqUqlIrwrkbSYIhwURJhDTktB67c2P" +
            "EdB0P1XCsrLy0veEse1UOqyWyVxDEoFbfnOvqSKky/0bi0llVokKyRWSO+WMp/m5yl5VGx454OlfFahRZqmYW9vT+jn7e/vK" +
            "9eHlVRr0NHRkRAwmqbh9PRUSceiHBQRK+R9ILLG2jLl6GfVaDRSu2nHx8cqAvno6FWUpmmpbFuo1WrKz92UVG7c9vY2V/+ia" +
            "RoIISp/5SflofhPNi9J3GYXVjeZgFKtVrkleCLDbR7d15PKjeTlAxR17sFQeNYVSUM8ZgYzsjAjG91XzrTg6H8X90Q9S3lQt" +
            "aU8Fn9nYK7m2a8bVpqnpKJ4zA66rittZX8UK8kEFMMwuM3bm6aZLSgq1oCfTqdct1dTSmVuElqnfpClKOXsDcNIZS/9eDzGa" +
            "DSSuf1hLRQt4AVpc/V+dSJd11NdamSaJizLAiFEdEmQZRrOFgedh3IJwTWEfRCmaQqNkFzXxf39PUzTxOHhoezk8kM1j4UqR" +
            "pTSBwhYeW8YBizLkrnncCHjJ4TIgvPn7KhKEJQuUiyMYxgGKKXS9jiGgVOr1UQuZ50QQuqzvwgax04FynQ6xXg8Vj6J8/fWV" +
            "6tVUd3aQkWPwCJsvAut6bouq3JQYu3s7KDRaKQ5D/PHfIG2ZQOSPV6fOBqNMgsE8PbZDwaDtCz8IqhiXiAUVoxywiNZUzyL" +
            "Dh2pXV9fpwEm8OFPreKEbdsyC9KkBobj+NnvZaMopTWxc2xrGY1G2DS5rovxeMzr7ZY+9EuhsAwzlrVYlqVsyMsrOkvLStZ" +
            "Ziu9bhnFykU0Wh+93vurFMNPB7ThP0yYr4SjEr3U1itdCYWZ2EfYTHcdRcQSWu2+JGYlNwKmGpG8tz2H+cFN9SRCYON1WmK" +
            "OiQkFhb9RCoaTdVj/MH4ZeYsTe8Fdxb2NpSAgJ7ZszcQBBxhX5AII4i/Fa4DAEkyO1oh7UGRnKjH95Lu73Wn2PsyAl1rJVN" +
            "kvWLMCs1M+4J9kVB6Wlo0QHpSVa4F1YTKD+SQIksaUUFhPoQ3pJ34TLVghmMSeIMXi5QWHvV16nbnPbn8LCvia8k6fzpAm" +
            "AJiGE20lMxdHnyaT+0ecBfqYHgeevCO6u2ry6K2FQZuB0IHgprADraEfN0pWCwsDU4S06y/JZ9RPWVfXT/iAhUGbgNOFN8p" +
            "xlDEYnra5KOpSMwREOQyqUuW6tDW8hgSqJ5xWAHs8QN1NQ5gC14I0+tyQAuoK3zu2Sd3ibaSgB3Zt/pdHFDeENC/UB9FUAo" +
            "TyUJTlPHd5QTh3vNZTP1tz4J3bdwKsT8KDihttMQsmb/gdve13vFEkE9wAAAABJRU5ErkJggg==";
        #endregion
    }
}
