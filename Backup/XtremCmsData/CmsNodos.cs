using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.CmsData
{
  public class CmsNodos
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodNodo;
    public string CodNodo { get { return pCodNodo; } set { pCodNodo = value; } }

    private string pCodNodoRel;
    public string CodNodoRel { get { return pCodNodoRel; } set { pCodNodoRel = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodTemplate;
    public string CodTemplate { get { return pCodTemplate; } set { pCodTemplate = value; } }

    private string pTituloNodo;
    public string TituloNodo { get { return pTituloNodo; } set { pTituloNodo = value; } }

    private string pTextoNodo;
    public string TextoNodo { get { return pTextoNodo; } set { pTextoNodo = value; } }

    private string pDateNodo;
    public string DateNodo { get { return pDateNodo; } set { pDateNodo = value; } }

    private string pEstNodo;
    public string EstNodo { get { return pEstNodo; } set { pEstNodo = value; } }

    private string pPrvNodo;
    public string PrvNodo { get { return pPrvNodo; } set { pPrvNodo = value; } }

    private string pIniNodo;
    public string IniNodo { get { return pIniNodo; } set { pIniNodo = value; } }

    private string pPfNodo;
    public string PfNodo { get { return pPfNodo; } set { pPfNodo = value; } }

    private string pTitleHeaderNodo;
    public string TitleHeaderNodo { get { return pTitleHeaderNodo; } set { pTitleHeaderNodo = value; } }

    private string pKeywordsNodo;
    public string KeywordsNodo { get { return pKeywordsNodo; } set { pKeywordsNodo = value; } }

    private string pContNodo;
    public string ContNodo { get { return pContNodo; } set { pContNodo = value; } }

    private string pOrdNodo;
    public string OrdNodo { get { return pOrdNodo; } set { pOrdNodo = value; } }

    private string pIndOrden;
    public string IndOrden { get { return pIndOrden; } set { pIndOrden = value; } }

    private string pIniAsocUsrNodo;
    public string IniAsocUsrNodo { get { return pIniAsocUsrNodo; } set { pIniAsocUsrNodo = value; } }

    private string pIndDesplUsrClient;
    public string IndDesplUsrClient { get { return pIndDesplUsrClient; } set { pIndDesplUsrClient = value; } }

    private string pIndOlvClaveNodo;
    public string IndOlvClaveNodo { get { return pIndOlvClaveNodo; } set { pIndOlvClaveNodo = value; } }

    private string pIndRstClaveNodo;
    public string IndRstClaveNodo { get { return pIndRstClaveNodo; } set { pIndRstClaveNodo = value; } }

    private string pIndLoginNodo;
    public string IndLoginNodo { get { return pIndLoginNodo; } set { pIndLoginNodo = value; } }

    private string pIndDesplUsrSite;
    public string IndDesplUsrSite { get { return pIndDesplUsrSite; } set { pIndDesplUsrSite = value; } }

    private string pIndPoltSecureNodo;
    public string IndPoltSecureNodo { get { return pIndPoltSecureNodo; } set { pIndPoltSecureNodo = value; } }

    private string pIndTermUseNodo;
    public string IndTermUseNodo { get { return pIndTermUseNodo; } set { pIndTermUseNodo = value; } }

    private string pIndRegistrateNodo;
    public string IndRegistrateNodo{ get { return pIndRegistrateNodo; } set { pIndRegistrateNodo = value; } }

    private string pIndPagExitoNodo;
    public string IndPagExitoNodo { get { return pIndPagExitoNodo; } set { pIndPagExitoNodo = value; } }

    private string pIndPhotoNodo;
    public string IndPhotoNodo { get { return pIndPhotoNodo; } set { pIndPhotoNodo = value; } }

    private string pIndIniNodoPhone;
    public string IndIniNodoPhone { get { return pIndIniNodoPhone; } set { pIndIniNodoPhone = value; } }

    private string pIndPfNodoPhone;
    public string IndPfNodoPhone { get { return pIndPfNodoPhone; } set { pIndPfNodoPhone = value; } }

    private string pIndContNodoPhone;
    public string IndContNodoPhone { get { return pIndContNodoPhone; } set { pIndContNodoPhone = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError = string.Empty;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public CmsNodos() { 

    }

    public CmsNodos(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public void Put()
    {
      DataTable dtData;
      oParam = new DBConn.SQLParameters(30);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              pCodNodo = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into cms_nodos(cod_nodo_rel, cod_usuario, cod_template, titulo_nodo, texto_nodo, date_nodo, est_nodo, prv_nodo, ini_nodo, pf_nodo, titleheader_nodo, keywords_nodo, cont_nodo, ord_nodo, ini_asoc_usr_nodo, ind_despl_usr_client, ind_olvclave_nodo, ind_rstclave_nodo, ind_login_nodo, ind_despl_usr_site, ind_poltsecure_nodo, ind_termuse_nodo, ind_registrate_nodo, ind_pagexito_nodo, ind_photo_nodo, ini_nodo_phone, pf_nodo_phone, cont_nodo_phone) values(");
              cSQL.Append("@cod_nodo_rel, @cod_usuario, @cod_template, @titulo_nodo, @texto_nodo, @date_nodo, @est_nodo, @prv_nodo, @ini_nodo, @pf_nodo, @titleheader_nodo, @keywords_nodo, @cont_nodo, @ord_nodo, @ini_asoc_usr_nodo, @ind_despl_usr_client, @ind_olvclave_nodo, @ind_rstclave_nodo, @ind_login_nodo, @ind_despl_usr_site, @ind_poltsecure_nodo, @ind_termuse_nodo, @ind_registrate_nodo, @ind_pagexito_nodo, @ind_photo_nodo, @ini_nodo_phone, @pf_nodo_phone, @cont_nodo_phone) ");
              oParam.AddParameters("@cod_nodo_rel", pCodNodoRel, TypeSQL.Numeric);
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_template", pCodTemplate, TypeSQL.Numeric);
              oParam.AddParameters("@titulo_nodo", pTituloNodo, TypeSQL.Varchar);
              oParam.AddParameters("@texto_nodo", pTextoNodo, TypeSQL.Text);
              oParam.AddParameters("@date_nodo", pDateNodo, TypeSQL.DateTime);
              oParam.AddParameters("@est_nodo", pEstNodo, TypeSQL.Char);
              oParam.AddParameters("@prv_nodo", pPrvNodo, TypeSQL.Int);
              oParam.AddParameters("@ini_nodo", pIniNodo, TypeSQL.Char);
              oParam.AddParameters("@pf_nodo", pPfNodo, TypeSQL.Char);
              oParam.AddParameters("@titleheader_nodo", pTitleHeaderNodo, TypeSQL.Varchar);
              oParam.AddParameters("@keywords_nodo", pKeywordsNodo, TypeSQL.Varchar);
              oParam.AddParameters("@cont_nodo", pContNodo, TypeSQL.Char);
              oParam.AddParameters("@ord_nodo", pOrdNodo, TypeSQL.Int);
              oParam.AddParameters("@ini_asoc_usr_nodo", pIniAsocUsrNodo, TypeSQL.Char);
              oParam.AddParameters("@ind_despl_usr_client", pIndDesplUsrClient, TypeSQL.Char);
              oParam.AddParameters("@ind_olvclave_nodo", pIndOlvClaveNodo, TypeSQL.Char);
              oParam.AddParameters("@ind_rstclave_nodo", pIndRstClaveNodo, TypeSQL.Char);
              oParam.AddParameters("@ind_login_nodo", pIndLoginNodo, TypeSQL.Char);
              oParam.AddParameters("@ind_despl_usr_site", pIndDesplUsrSite, TypeSQL.Char);
              oParam.AddParameters("@ind_poltsecure_nodo", pIndPoltSecureNodo, TypeSQL.Char);
              oParam.AddParameters("@ind_termuse_nodo", pIndTermUseNodo, TypeSQL.Char);
              oParam.AddParameters("@ind_registrate_nodo", pIndRegistrateNodo, TypeSQL.Char);
              oParam.AddParameters("@ind_pagexito_nodo", pIndPagExitoNodo, TypeSQL.Char);
              oParam.AddParameters("@ind_photo_nodo", pIndPhotoNodo, TypeSQL.Char);
              oParam.AddParameters("@ini_nodo_phone", pIndIniNodoPhone, TypeSQL.Char);
              oParam.AddParameters("@pf_nodo_phone", pIndPfNodoPhone, TypeSQL.Char);
              oParam.AddParameters("@cont_nodo_phone", pIndContNodoPhone, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodNodo = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update cms_nodos set ");
              if (!string.IsNullOrEmpty(pCodNodoRel)) {
                cSQL.Append(" cod_nodo_rel = @cod_nodo_rel ");
                oParam.AddParameters("@cod_nodo_rel", pCodNodoRel, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pCodTemplate))
              {
                cSQL.Append(sComa);
                cSQL.Append(" cod_template = @cod_template ");
                oParam.AddParameters("@cod_template", pCodTemplate, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTituloNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" titulo_nodo = @titulo_nodo ");
                oParam.AddParameters("@titulo_nodo", pTituloNodo, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTextoNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" texto_nodo = @texto_nodo ");
                oParam.AddParameters("@texto_nodo", pTextoNodo, TypeSQL.Text);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pDateNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" date_nodo = @date_nodo ");
                oParam.AddParameters("@date_nodo", pDateNodo, TypeSQL.DateTime);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_nodo = @est_nodo ");
                oParam.AddParameters("@est_nodo", pEstNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pPrvNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" prv_nodo = @prv_nodo ");
                oParam.AddParameters("@prv_nodo", pPrvNodo, TypeSQL.Int);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIniNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ini_nodo = @ini_nodo ");
                oParam.AddParameters("@ini_nodo", pIniNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pPfNodo)) {
                cSQL.Append(sComa);
                cSQL.Append(" pf_nodo = @pf_nodo ");
                oParam.AddParameters("@pf_nodo", pPfNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTitleHeaderNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" titleheader_nodo = @titleheader_nodo ");
                oParam.AddParameters("@titleheader_nodo", pTitleHeaderNodo, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pKeywordsNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" keywords_nodo = @keywords_nodo ");
                oParam.AddParameters("@keywords_nodo", pKeywordsNodo, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pContNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" cont_nodo = @cont_nodo ");
                oParam.AddParameters("@cont_nodo", pContNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pOrdNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ord_nodo = @ord_nodo ");
                oParam.AddParameters("@ord_nodo", pOrdNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIniAsocUsrNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ini_asoc_usr_nodo = @ini_asoc_usr_nodo ");
                oParam.AddParameters("@ini_asoc_usr_nodo", pIniAsocUsrNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndDesplUsrClient))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_despl_usr_client = @ind_despl_usr_client ");
                oParam.AddParameters("@ind_despl_usr_client", pIndDesplUsrClient, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndOlvClaveNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_olvclave_nodo = @ind_olvclave_nodo ");
                oParam.AddParameters("@ind_olvclave_nodo", pIndOlvClaveNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndRstClaveNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_rstclave_nodo = @ind_rstclave_nodo ");
                oParam.AddParameters("@ind_rstclave_nodo", pIndRstClaveNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndLoginNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_login_nodo = @ind_login_nodo ");
                oParam.AddParameters("@ind_login_nodo", pIndLoginNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndDesplUsrSite))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_despl_usr_site = @ind_despl_usr_site ");
                oParam.AddParameters("@ind_despl_usr_site", pIndDesplUsrSite, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndPoltSecureNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_poltsecure_nodo = @ind_poltsecure_nodo ");
                oParam.AddParameters("@ind_poltsecure_nodo", pIndPoltSecureNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndTermUseNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_termuse_nodo = @ind_termuse_nodo ");
                oParam.AddParameters("@ind_termuse_nodo", pIndTermUseNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndRegistrateNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_registrate_nodo = @ind_registrate_nodo ");
                oParam.AddParameters("@ind_registrate_nodo", pIndRegistrateNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndPagExitoNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_pagexito_nodo = @ind_pagexito_nodo ");
                oParam.AddParameters("@ind_pagexito_nodo", pIndPagExitoNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndPhotoNodo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_photo_nodo = @ind_photo_nodo ");
                oParam.AddParameters("@ind_photo_nodo", pIndPhotoNodo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndIniNodoPhone))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ini_nodo_phone = @ini_nodo_phone ");
                oParam.AddParameters("@ini_nodo_phone", pIndIniNodoPhone, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndPfNodoPhone))
              {
                cSQL.Append(sComa);
                cSQL.Append(" pf_nodo_phone = @pf_nodo_phone ");
                oParam.AddParameters("@pf_nodo_phone", pIndPfNodoPhone, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndContNodoPhone))
              {
                cSQL.Append(sComa);
                cSQL.Append(" cont_nodo_phone = @cont_nodo_phone ");
                oParam.AddParameters("@cont_nodo_phone", pIndContNodoPhone, TypeSQL.Char);
                sComa = ", ";
              }
              
              cSQL.Append(" where cod_nodo = @cod_nodo ");
              oParam.AddParameters("@cod_nodo", pCodNodo, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from cms_nodos where cod_nodo = @cod_nodo ");
              oParam.AddParameters("@cod_nodo", pCodNodo, TypeSQL.Numeric);
              oConn.Delete(cSQL.ToString(), oParam);

              break;
          }
        }
        catch (Exception Ex)
        {
          pError = Ex.Message;
        }
      }
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_nodo, cod_nodo_rel, cod_usuario, cod_template, titulo_nodo, texto_nodo, date_nodo, est_nodo, prv_nodo, ini_nodo, pf_nodo, titleheader_nodo, keywords_nodo, cont_nodo, ord_nodo, ini_asoc_usr_nodo, ind_despl_usr_client, ind_olvclave_nodo, ind_rstclave_nodo, ind_login_nodo, ind_despl_usr_site, ind_poltsecure_nodo, ind_termuse_nodo, ind_registrate_nodo, ind_pagexito_nodo, ind_photo_nodo, ini_nodo_phone, pf_nodo_phone, cont_nodo_phone ");
        cSQL.Append("from cms_nodos ");

        if (!string.IsNullOrEmpty(pCodNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_nodo = @cod_nodo");
          oParam.AddParameters("@cod_nodo", pCodNodo, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodNodoRel))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_nodo_rel = @cod_nodo_rel");
          oParam.AddParameters("@cod_nodo_rel", pCodNodoRel, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pTituloNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" titulo_nodo = %@titulo_nodo%");
          oParam.AddParameters("@titulo_nodo", pTituloNodo, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pPfNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" pf_nodo = @pf_nodo ");
          oParam.AddParameters("@pf_nodo", pPfNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pContNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cont_nodo = @cont_nodo ");
          oParam.AddParameters("@cont_nodo", pContNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndOlvClaveNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_olvclave_nodo = @ind_olvclave_nodo ");
          oParam.AddParameters("@ind_olvclave_nodo", pIndOlvClaveNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndRstClaveNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_rstclave_nodo = @ind_rstclave_nodo ");
          oParam.AddParameters("@ind_rstclave_nodo", pIndRstClaveNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndLoginNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_login_nodo = @ind_login_nodo ");
          oParam.AddParameters("@ind_login_nodo", pIndLoginNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndPoltSecureNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_poltsecure_nodo = @ind_poltsecure_nodo ");
          oParam.AddParameters("@ind_poltsecure_nodo", pIndPoltSecureNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndTermUseNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_termuse_nodo = @ind_termuse_nodo ");
          oParam.AddParameters("@ind_termuse_nodo", pIndTermUseNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndRegistrateNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_registrate_nodo = @ind_registrate_nodo ");
          oParam.AddParameters("@ind_registrate_nodo", pIndRegistrateNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndPagExitoNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_pagexito_nodo = @ind_pagexito_nodo ");
          oParam.AddParameters("@ind_pagexito_nodo", pIndPagExitoNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndPhotoNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_photo_nodo = @ind_photo_nodo ");
          oParam.AddParameters("@ind_photo_nodo", pIndPhotoNodo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndIniNodoPhone))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ini_nodo_phone = @ini_nodo_phone ");
          oParam.AddParameters("@ini_nodo_phone", pIndIniNodoPhone, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndPfNodoPhone))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" pf_nodo_phone = @pf_nodo_phone ");
          oParam.AddParameters("@pf_nodo_phone", pIndPfNodoPhone, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndContNodoPhone))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cont_nodo_phone = @cont_nodo_phone ");
          oParam.AddParameters("@cont_nodo_phone", pIndContNodoPhone, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndOrden)) {
          cSQL.Append(pIndOrden);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        pError = "Conexion Cerrada";
        return null;
      }
    }

    public string SerializaNodo(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return string.Empty;

      try
      {
        BinaryNodo oNodo = new BinaryNodo();
        CmsNodos oNodos = new CmsNodos(ref oConn);
        oNodos.CodNodo = pCodNodo;
        DataTable oData = oNodos.Get();

        if (oData != null)
          if (oData.Rows.Count > 0)
          {
            oNodo.CodNodo = oData.Rows[0]["cod_nodo"].ToString();
            oNodo.CodNodoRel = oData.Rows[0]["cod_nodo_rel"].ToString();
            oNodo.CodUsuario = oData.Rows[0]["cod_usuario"].ToString();
            oNodo.CodTemplate = oData.Rows[0]["cod_template"].ToString();
            oNodo.TituloNodo = oData.Rows[0]["titulo_nodo"].ToString();
            oNodo.TextoNodo = oData.Rows[0]["texto_nodo"].ToString();
            oNodo.DateNodo = oData.Rows[0]["date_nodo"].ToString();
            oNodo.EstNodo = oData.Rows[0]["est_nodo"].ToString();
            oNodo.PrvNodo = oData.Rows[0]["prv_nodo"].ToString();
            oNodo.IniNodo = oData.Rows[0]["ini_nodo"].ToString();
            oNodo.PfNodo = oData.Rows[0]["pf_nodo"].ToString();
            oNodo.TitleHeaderNodo = oData.Rows[0]["titleheader_nodo"].ToString();
            oNodo.KeywordsNodo = oData.Rows[0]["keywords_nodo"].ToString();
            oNodo.ContNodo = oData.Rows[0]["cont_nodo"].ToString();
            oNodo.OrdNodo = oData.Rows[0]["ord_nodo"].ToString();
            oNodo.IniAsocUsrNodo = oData.Rows[0]["ini_asoc_usr_nodo"].ToString();
            oNodo.IndDesplUsrClient = oData.Rows[0]["ind_despl_usr_client"].ToString();
            oNodo.IndOlvClaveNodo = oData.Rows[0]["ind_olvclave_nodo"].ToString();
            oNodo.IndRstClaveNodo = oData.Rows[0]["ind_rstclave_nodo"].ToString();
            oNodo.IndLoginNodo = oData.Rows[0]["ind_login_nodo"].ToString();
            oNodo.IndDesplUsrSite = oData.Rows[0]["ind_despl_usr_site"].ToString();
            oNodo.IndPoltSecureNodo = oData.Rows[0]["ind_poltsecure_nodo"].ToString();
            oNodo.IndTermUseNodo = oData.Rows[0]["ind_termuse_nodo"].ToString();
            oNodo.IndRegistrateNodo = oData.Rows[0]["ind_registrate_nodo"].ToString();
            oNodo.IndPagExitoNodo = oData.Rows[0]["ind_pagexito_nodo"].ToString();
            oNodo.IndPhotoNodo = oData.Rows[0]["ind_photo_nodo"].ToString();
            oNodo.IndIniNodoPhone = oData.Rows[0]["ini_nodo_phone"].ToString();
            oNodo.IndPfNodoPhone = oData.Rows[0]["pf_nodo_phone"].ToString();
            oNodo.IndContNodoPhone = oData.Rows[0]["cont_nodo_phone"].ToString();
          }
        oData.Dispose();
        oNodos = null;

        if (Directory.Exists(cPath) && !string.IsNullOrEmpty(cFile))
        {
          IFormatter oBinFormat = new BinaryFormatter();
          Stream oFileStream = new FileStream(cPath + cFile, FileMode.Create, FileAccess.Write);
          oBinFormat.Serialize(oFileStream, oNodo);
          oFileStream.Close();

          oFileStream = null;
          oNodo = null;
        }
        return string.Empty;
      }
      catch (Exception Ex)
      {
        return Ex.Source + " - " + Ex.Message + " - " + Ex.StackTrace;
      }
    }

    public BinaryNodo ClassGet()
    {
      try
      {
        if (!string.IsNullOrEmpty(pCodNodo))
        {
          StringBuilder Directorio = new StringBuilder();
          StringBuilder Archivo = new StringBuilder();

          Directorio.Append(cPath);
          Directorio.Append(@"\binary\");

          Archivo.Append("nodo_");
          Archivo.Append(pCodNodo);
          Archivo.Append(".bin");

          if (File.Exists(Directorio.ToString() + Archivo.ToString()))
          {
            IFormatter oBinFormat = new BinaryFormatter();
            Stream oFileStream = new FileStream(Directorio.ToString() + Archivo.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryNodo oNodo = (BinaryNodo)oBinFormat.Deserialize(oFileStream);
            oFileStream.Close();
            return oNodo;
          }
          else
            return new BinaryNodo();
        }
        else
          return new BinaryNodo();
      }
      catch
      {
        return new BinaryNodo();
      }
    }

    public void SerializaTblNodo(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        BinaryNodo bNodo = new BinaryNodo();
        CmsNodos oNodos = new CmsNodos(ref oConn);
        DataTable oData = oNodos.Get();

        if (Directory.Exists(cPath) && !string.IsNullOrEmpty(cFile) && oData != null)
        {
          IFormatter oBinFormat = new BinaryFormatter();
          Stream oFileStream = new FileStream(cPath + cFile, FileMode.Create, FileAccess.Write);
          oBinFormat.Serialize(oFileStream, oData);
          oFileStream.Close();
          oFileStream = null;
          oData.Dispose();
        }

      }
      catch (Exception Ex)
      {
        //return Ex.Source + " - " + Ex.Message + " - " + Ex.StackTrace;
      }
    }    

  }

  [Serializable]
  public class BinaryNodo
  {
    public string CodNodo = string.Empty;
    public string CodNodoRel = string.Empty;
    public string CodUsuario = string.Empty;
    public string CodTemplate = string.Empty;
    public string TituloNodo = string.Empty;
    public string TextoNodo = string.Empty;
    public string DateNodo = string.Empty;
    public string EstNodo = string.Empty;
    public string PrvNodo = string.Empty;
    public string IniNodo = string.Empty;
    public string PfNodo = string.Empty;
    public string TitleHeaderNodo = string.Empty;
    public string KeywordsNodo = string.Empty;
    public string ContNodo = string.Empty;
    public string OrdNodo = string.Empty;
    public string IniAsocUsrNodo = string.Empty;
    public string IndDesplUsrClient = string.Empty;
    public string IndOlvClaveNodo = string.Empty;
    public string IndRstClaveNodo = string.Empty;
    public string IndLoginNodo = string.Empty;
    public string IndDesplUsrSite = string.Empty;
    public string IndPoltSecureNodo = string.Empty;
    public string IndTermUseNodo = string.Empty;
    public string IndRegistrateNodo = string.Empty;
    public string IndPagExitoNodo = string.Empty;
    public string IndPhotoNodo = string.Empty;
    public string IndIniNodoPhone = string.Empty;
    public string IndPfNodoPhone = string.Empty;
    public string IndContNodoPhone = string.Empty;
  }
}
