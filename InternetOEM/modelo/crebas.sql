/* ============================================================ */
/*   Database name:  Modelo_Adm_MeetSite                        */
/*   DBMS name:      Microsoft SQL Server 6.x                   */
/*   Created on:     11/09/2016  20:39                          */
/* ============================================================ */

if exists (select 1
            from  sysobjects
           where  id = object_id('app_img_banner')
            and   type = 'U')
   drop table app_img_banner
go

if exists (select 1
            from  sysobjects
           where  id = object_id('apt_preg_ranking')
            and   type = 'U')
   drop table apt_preg_ranking
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_emailtouser')
            and   type = 'U')
   drop table sys_emailtouser
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_seguir_usuarios')
            and   type = 'U')
   drop table sys_seguir_usuarios
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_metadata_valor')
            and   name  = 'cod_metadata'
            and   indid > 0
            and   indid < 255)
   drop index cmr_metadata_valor.cod_metadata
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_metadata_valor')
            and   name  = 'cod_valor_metadata'
            and   indid > 0
            and   indid < 255)
   drop index cmr_metadata_valor.cod_valor_metadata
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cmr_metadata_valor')
            and   type = 'U')
   drop table cmr_metadata_valor
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_cont_metadata')
            and   name  = 'cod_contenido'
            and   indid > 0
            and   indid < 255)
   drop index cmr_cont_metadata.cod_contenido
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_cont_metadata')
            and   name  = 'cod_metadata'
            and   indid > 0
            and   indid < 255)
   drop index cmr_cont_metadata.cod_metadata
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_cont_metadata')
            and   name  = 'cod_valor_metadata'
            and   indid > 0
            and   indid < 255)
   drop index cmr_cont_metadata.cod_valor_metadata
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cmr_cont_metadata')
            and   type = 'U')
   drop table cmr_cont_metadata
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_comentario_usuario')
            and   name  = 'cod_usuario'
            and   indid > 0
            and   indid < 255)
   drop index sys_comentario_usuario.cod_usuario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_comentario_usuario')
            and   name  = 'cod_usuario_rel'
            and   indid > 0
            and   indid < 255)
   drop index sys_comentario_usuario.cod_usuario_rel
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_comentario_usuario')
            and   type = 'U')
   drop table sys_comentario_usuario
go

if exists (select 1
            from  sysobjects
           where  id = object_id('syr_campo_opciones')
            and   type = 'U')
   drop table syr_campo_opciones
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_relacion_usuarios')
            and   name  = 'cod_usuario_rel'
            and   indid > 0
            and   indid < 255)
   drop index sys_relacion_usuarios.cod_usuario_rel
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_relacion_usuarios')
            and   name  = 'est_relacion'
            and   indid > 0
            and   indid < 255)
   drop index sys_relacion_usuarios.est_relacion
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_relacion_usuarios')
            and   type = 'U')
   drop table sys_relacion_usuarios
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_archivos_usuarios')
            and   name  = 'cod_usuario'
            and   indid > 0
            and   indid < 255)
   drop index sys_archivos_usuarios.cod_usuario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_archivos_usuarios')
            and   name  = 'nom_archivo'
            and   indid > 0
            and   indid < 255)
   drop index sys_archivos_usuarios.nom_archivo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_archivos_usuarios')
            and   name  = 'tip_archivo'
            and   indid > 0
            and   indid < 255)
   drop index sys_archivos_usuarios.tip_archivo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_archivos_usuarios')
            and   type = 'U')
   drop table sys_archivos_usuarios
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_contenido_grupos')
            and   name  = 'cod_contenido'
            and   indid > 0
            and   indid < 255)
   drop index cmr_contenido_grupos.cod_contenido
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_contenido_grupos')
            and   name  = 'cod_grupo'
            and   indid > 0
            and   indid < 255)
   drop index cmr_contenido_grupos.cod_grupo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_contenido_grupos')
            and   name  = 'tipo'
            and   indid > 0
            and   indid < 255)
   drop index cmr_contenido_grupos.tipo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cmr_contenido_grupos')
            and   type = 'U')
   drop table cmr_contenido_grupos
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_contenido_usuario')
            and   name  = 'cod_contenido'
            and   indid > 0
            and   indid < 255)
   drop index cmr_contenido_usuario.cod_contenido
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_contenido_usuario')
            and   name  = 'cod_usuario'
            and   indid > 0
            and   indid < 255)
   drop index cmr_contenido_usuario.cod_usuario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_contenido_usuario')
            and   name  = 'tipo'
            and   indid > 0
            and   indid < 255)
   drop index cmr_contenido_usuario.tipo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cmr_contenido_usuario')
            and   type = 'U')
   drop table cmr_contenido_usuario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_nodos_grupos')
            and   name  = 'cod_grupo'
            and   indid > 0
            and   indid < 255)
   drop index cmr_nodos_grupos.cod_grupo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_nodos_grupos')
            and   name  = 'COD_NODO'
            and   indid > 0
            and   indid < 255)
   drop index cmr_nodos_grupos.COD_NODO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_nodos_grupos')
            and   name  = 'tipo'
            and   indid > 0
            and   indid < 255)
   drop index cmr_nodos_grupos.tipo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cmr_nodos_grupos')
            and   type = 'U')
   drop table cmr_nodos_grupos
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_nodos_usuarios')
            and   name  = 'cod_usuario'
            and   indid > 0
            and   indid < 255)
   drop index cmr_nodos_usuarios.cod_usuario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_nodos_usuarios')
            and   name  = 'COD_NODO'
            and   indid > 0
            and   indid < 255)
   drop index cmr_nodos_usuarios.COD_NODO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cmr_nodos_usuarios')
            and   name  = 'tipo'
            and   indid > 0
            and   indid < 255)
   drop index cmr_nodos_usuarios.tipo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cmr_nodos_usuarios')
            and   type = 'U')
   drop table cmr_nodos_usuarios
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('syr_info_usuarios')
            and   name  = 'cod_campo'
            and   indid > 0
            and   indid < 255)
   drop index syr_info_usuarios.cod_campo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('syr_info_usuarios')
            and   name  = 'cod_usuario'
            and   indid > 0
            and   indid < 255)
   drop index syr_info_usuarios.cod_usuario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('syr_info_usuarios')
            and   name  = 'tip_campo'
            and   indid > 0
            and   indid < 255)
   drop index syr_info_usuarios.tip_campo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('syr_info_usuarios')
            and   type = 'U')
   drop table syr_info_usuarios
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_archivos')
            and   name  = 'cod_contenido'
            and   indid > 0
            and   indid < 255)
   drop index cms_archivos.cod_contenido
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_archivos')
            and   name  = 'cod_archivo_rel'
            and   indid > 0
            and   indid < 255)
   drop index cms_archivos.cod_archivo_rel
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cms_archivos')
            and   type = 'U')
   drop table cms_archivos
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_perfiles_usuarios')
            and   type = 'U')
   drop table sys_perfiles_usuarios
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_grupo_usuarios')
            and   type = 'U')
   drop table sys_grupo_usuarios
go

if exists (select 1
            from  sysobjects
           where  id = object_id('app_ranking')
            and   type = 'U')
   drop table app_ranking
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_contenidos')
            and   name  = 'cod_usuario'
            and   indid > 0
            and   indid < 255)
   drop index cms_contenidos.cod_usuario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_contenidos')
            and   name  = 'cod_nodo'
            and   indid > 0
            and   indid < 255)
   drop index cms_contenidos.cod_nodo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_contenidos')
            and   name  = 'cod_contenido_rel'
            and   indid > 0
            and   indid < 255)
   drop index cms_contenidos.cod_contenido_rel
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_contenidos')
            and   name  = 'cod_usuario_rel'
            and   indid > 0
            and   indid < 255)
   drop index cms_contenidos.cod_usuario_rel
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_contenidos')
            and   name  = 'dest_contenido'
            and   indid > 0
            and   indid < 255)
   drop index cms_contenidos.dest_contenido
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_contenidos')
            and   name  = 'est_contenido'
            and   indid > 0
            and   indid < 255)
   drop index cms_contenidos.est_contenido
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_contenidos')
            and   name  = 'prv_contendio'
            and   indid > 0
            and   indid < 255)
   drop index cms_contenidos.prv_contendio
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_contenidos')
            and   name  = 'ind_rss'
            and   indid > 0
            and   indid < 255)
   drop index cms_contenidos.ind_rss
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cms_contenidos')
            and   type = 'U')
   drop table cms_contenidos
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'cod_nodo_rel'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.cod_nodo_rel
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'cod_usuario'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.cod_usuario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'cod_template'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.cod_template
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'est_nodo'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.est_nodo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'prv_nodo'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.prv_nodo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'ini_nodo'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.ini_nodo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'pf_nodo'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.pf_nodo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'cont_nodo'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.cont_nodo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cms_nodos')
            and   name  = 'ord_nodo'
            and   indid > 0
            and   indid < 255)
   drop index cms_nodos.ord_nodo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cms_nodos')
            and   type = 'U')
   drop table cms_nodos
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_usuario')
            and   name  = 'nom_user'
            and   indid > 0
            and   indid < 255)
   drop index sys_usuario.nom_user
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_usuario')
            and   name  = 'ape_user'
            and   indid > 0
            and   indid < 255)
   drop index sys_usuario.ape_user
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_usuario')
            and   name  = 'eml_user'
            and   indid > 0
            and   indid < 255)
   drop index sys_usuario.eml_user
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sys_usuario')
            and   name  = 'login_user'
            and   indid > 0
            and   indid < 255)
   drop index sys_usuario.login_user
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_usuario')
            and   type = 'U')
   drop table sys_usuario
go

if exists (select 1
            from  sysobjects
           where  id = object_id('app_banner')
            and   type = 'U')
   drop table app_banner
go

if exists (select 1
            from  sysobjects
           where  id = object_id('app_preg_ranking')
            and   type = 'U')
   drop table app_preg_ranking
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cms_valor_metadata')
            and   type = 'U')
   drop table cms_valor_metadata
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cms_metadata')
            and   type = 'U')
   drop table cms_metadata
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_opciones_campo')
            and   type = 'U')
   drop table sys_opciones_campo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_tipo_usuario')
            and   type = 'U')
   drop table sys_tipo_usuario
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_campo_usuarios')
            and   type = 'U')
   drop table sys_campo_usuarios
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cms_template')
            and   type = 'U')
   drop table cms_template
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_perfiles')
            and   type = 'U')
   drop table sys_perfiles
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_grupos')
            and   type = 'U')
   drop table sys_grupos
go

if exists (select 1
            from  sysobjects
           where  id = object_id('app_campana')
            and   type = 'U')
   drop table app_campana
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_log')
            and   type = 'U')
   drop table sys_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_param_email')
            and   type = 'U')
   drop table sys_param_email
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_tbl_code')
            and   type = 'U')
   drop table sys_tbl_code
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cms_zona')
            and   type = 'U')
   drop table cms_zona
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sys_parametros')
            and   type = 'U')
   drop table sys_parametros
go

/* ============================================================ */
/*   Table: sys_parametros                                      */
/* ============================================================ */
create table sys_parametros
(
    cod_codigo             numeric(25)           not null,
    nom_parametro          varchar(255)          null    ,
    valor_parametro        varchar(255)          null    ,
    show_parametro         char(1)               null    ,
    constraint PK_SYS_PARAMETROS primary key (cod_codigo)
)
go

/* ============================================================ */
/*   Table: cms_zona                                            */
/* ============================================================ */
create table cms_zona
(
    cod_zona               numeric               identity,
    nom_zona               varchar(100)          null    ,
    texto_zona             text                  null    ,
    est_zona               char(1)               null    ,
    ind_desp_cont          char(1)               null    ,
    timestamp              timestamp             null    ,
    constraint PK_CMS_ZONA primary key (cod_zona)
)
go

/* ============================================================ */
/*   Table: sys_tbl_code                                        */
/* ============================================================ */
create table sys_tbl_code
(
    tabla                  varchar(255)          null    ,
    code                   numeric(25)           null    
)
go

/* ============================================================ */
/*   Table: sys_param_email                                     */
/* ============================================================ */
create table sys_param_email
(
    cod_email              numeric(25)           not null,
    nom_email              varchar(255)          null    ,
    asunto_email           varchar(255)          null    ,
    cuerpo_email           text                  null    ,
    tipo_email             char(1)               null    ,
    constraint PK_SYS_PARAM_EMAIL primary key (cod_email)
)
go

/* ============================================================ */
/*   Table: sys_log                                             */
/* ============================================================ */
create table sys_log
(
    cod_log                numeric               identity,
    ip_usuario             varchar(20)           null    ,
    id_usuario             numeric(10)           null    ,
    fch_log                datetime              null    ,
    pag_log                varchar(400)          null    ,
    cod_evt_log            numeric(10)           null    ,
    obs_log                text                  null    ,
    constraint PK_SYS_LOG primary key (cod_log)
)
go

/* ============================================================ */
/*   Table: app_campana                                         */
/* ============================================================ */
create table app_campana
(
    cod_campana            numeric               identity,
    nom_campana            varchar(255)          null    ,
    text_campana           text                  null    ,
    fch_ini_campana        datetime              null    ,
    fch_ter_campana        datetime              null    ,
    est_campana            char(1)               null    
)
go

/* ============================================================ */
/*   Table: sys_grupos                                          */
/* ============================================================ */
create table sys_grupos
(
    cod_grp                numeric(10)           not null,
    nom_grp                varchar(255)          null    ,
    est_grp                char(1)               null    ,
    timestamp              timestamp             null    ,
    constraint PK_SYS_GRUPOS primary key (cod_grp)
)
go

/* ============================================================ */
/*   Table: sys_perfiles                                        */
/* ============================================================ */
create table sys_perfiles
(
    cod_perfil             numeric(10)           not null,
    nom_perfil             varchar(225)          null    ,
    est_perfil             char(1)               null    ,
    timestamp              timestamp             null    ,
    constraint PK_SYS_PERFILES primary key (cod_perfil)
)
go

/* ============================================================ */
/*   Table: cms_template                                        */
/* ============================================================ */
create table cms_template
(
    cod_template           numeric               identity,
    nom_template           varchar(100)          null    ,
    texto_template         text                  null    ,
    arch_template          varchar(200)          null    ,
    est_template           char(1)               null    ,
    timestamp              timestamp             null    ,
    constraint PK_CMS_TEMPLATE primary key (cod_template)
)
go

/* ============================================================ */
/*   Table: sys_campo_usuarios                                  */
/* ============================================================ */
create table sys_campo_usuarios
(
    cod_campo              numeric(25)           not null,
    nom_campo              varchar(255)          null    ,
    tipo_campo             char(3)               null    ,
    est_campo              char(1)               null    ,
    desp_campo             char(1)               null    ,
    ord_campo              integer               null    ,
    ind_despliegue         char(1)               null    ,
    ind_despliegue_portal  char(1)               null    ,
    ind_validacion         char(1)               null    ,
    constraint PK_SYS_CAMPO_USUARIOS primary key (cod_campo)
)
go

/* ============================================================ */
/*   Table: sys_tipo_usuario                                    */
/* ============================================================ */
create table sys_tipo_usuario
(
    cod_tipo               numeric(25)           not null,
    nom_tipo               varchar(255)          null    ,
    constraint PK_SYS_TIPO_USUARIO primary key (cod_tipo)
)
go

/* ============================================================ */
/*   Table: sys_opciones_campo                                  */
/* ============================================================ */
create table sys_opciones_campo
(
    cod_opcion             numeric(25)           not null,
    nom_opcion             varchar(255)          null    ,
    constraint PK_SYS_OPCIONES_CAMPO primary key (cod_opcion)
)
go

/* ============================================================ */
/*   Table: cms_metadata                                        */
/* ============================================================ */
create table cms_metadata
(
    cod_metadata           numeric               identity,
    nom_metadata           varchar(255)          null    ,
    tipo_metadata          char(1)               null    ,
    est_metadata           char(1)               null    ,
    timestamp              timestamp             null    ,
    constraint PK_CMS_METADATA primary key (cod_metadata)
)
go

/* ============================================================ */
/*   Table: cms_valor_metadata                                  */
/* ============================================================ */
create table cms_valor_metadata
(
    cod_valor_metadaa      numeric               identity,
    valor_metadata         varchar(4000)         null    ,
    timestamp              timestamp             null    ,
    constraint PK_CMS_VALOR_METADATA primary key (cod_valor_metadaa)
)
go

/* ============================================================ */
/*   Table: app_preg_ranking                                    */
/* ============================================================ */
create table app_preg_ranking
(
    cod_preg_ranking       numeric               identity,
    preg_ranking           varchar(255)          null    ,
    est_preg_ranking       char(1)               null    ,
    constraint PK_APP_PREG_RANKING primary key (cod_preg_ranking)
)
go

/* ============================================================ */
/*   Table: app_banner                                          */
/* ============================================================ */
create table app_banner
(
    cod_banner             numeric               identity,
    nom_banner             varchar(255)          null    ,
    tipo_banner            char(1)               null    ,
    est_banner             char(1)               null    ,
    constraint PK_APP_BANNER primary key (cod_banner)
)
go

/* ============================================================ */
/*   Table: sys_usuario                                         */
/* ============================================================ */
create table sys_usuario
(
    cod_user               numeric(10)           not null,
    cod_tipo               numeric(25)           null    ,
    nom_user               varchar(225)          null    ,
    ape_user               varchar(225)          null    ,
    eml_user               varchar(100)          null    ,
    login_user             varchar(100)          null    ,
    pwd_user               varchar(255)          null    ,
    est_user               char(1)               null    ,
    date_creacion          datetime              null    ,
    date_modificacion      datetime              null    ,
    fono_usuario           varchar(50)           null    ,
    timestamp              timestamp             null    ,
    dest_user              char(1)               null    ,
    ind_validado           char(1)               null    ,
    nota_ranking           decimal               null    ,
    notetarget             char(1)               null    ,
    constraint PK_SYS_USUARIO primary key (cod_user)
)
go

/* ============================================================ */
/*   Index: nom_user                                            */
/* ============================================================ */
create index nom_user on sys_usuario (nom_user)
go

/* ============================================================ */
/*   Index: ape_user                                            */
/* ============================================================ */
create index ape_user on sys_usuario (ape_user)
go

/* ============================================================ */
/*   Index: eml_user                                            */
/* ============================================================ */
create index eml_user on sys_usuario (eml_user)
go

/* ============================================================ */
/*   Index: login_user                                          */
/* ============================================================ */
create index login_user on sys_usuario (login_user)
go

/* ============================================================ */
/*   Table: cms_nodos                                           */
/* ============================================================ */
create table cms_nodos
(
    cod_nodo               numeric               identity,
    cod_nodo_rel           numeric               null    ,
    cod_user               numeric(10)           null    ,
    cod_template           numeric               null    ,
    titulo_nodo            varchar(500)          null    ,
    texto_nodo             text                  null    ,
    date_nodo              datetime              null    ,
    est_nodo               char(1)               null    ,
    prv_nodo               integer               null    ,
    ini_nodo               char(1)               null    ,
    pf_nodo                char(1)               null    ,
    titleheader_nodo       varchar(500)          null    ,
    keywords_nodo          varchar(500)          null    ,
    cont_nodo              char(1)               null    ,
    ord_nodo               integer               null    ,
    timestamp              timestamp             null    ,
    ini_asoc_usr_nodo      char(1)               null    ,
    ind_despl_usr_client   char(1)               null    ,
    ind_olvclave_nodo      char(1)               null    ,
    ind_login_nodo         char(1)               null    ,
    ind_despl_usr_site     char(1)               null    ,
    ind_poltsecure_nodo    char(1)               null    ,
    ind_termuse_nodo       char(1)               null    ,
    ind_registrate_nodo    char(1)               null    ,
    ind_photo_nodo         char(1)               null    ,
    ini_nodo_phone         char(1)               null    ,
    pf_nodo_phone          char(1)               null    ,
    cont_nodo_phone        char(1)               null    ,
    ind_rstclave_nodo      char(1)               null    ,
    ind_pagexito_nodo      char(1)               null    ,
    constraint PK_CMS_NODOS primary key (cod_nodo)
)
go

/* ============================================================ */
/*   Index: cod_nodo_rel                                        */
/* ============================================================ */
create index cod_nodo_rel on cms_nodos (cod_nodo_rel)
go

/* ============================================================ */
/*   Index: cod_usuario                                         */
/* ============================================================ */
create index cod_usuario on cms_nodos (cod_user)
go

/* ============================================================ */
/*   Index: cod_template                                        */
/* ============================================================ */
create index cod_template on cms_nodos (cod_template)
go

/* ============================================================ */
/*   Index: est_nodo                                            */
/* ============================================================ */
create index est_nodo on cms_nodos (est_nodo)
go

/* ============================================================ */
/*   Index: prv_nodo                                            */
/* ============================================================ */
create index prv_nodo on cms_nodos (prv_nodo)
go

/* ============================================================ */
/*   Index: ini_nodo                                            */
/* ============================================================ */
create index ini_nodo on cms_nodos (ini_nodo)
go

/* ============================================================ */
/*   Index: pf_nodo                                             */
/* ============================================================ */
create index pf_nodo on cms_nodos (pf_nodo)
go

/* ============================================================ */
/*   Index: cont_nodo                                           */
/* ============================================================ */
create index cont_nodo on cms_nodos (cont_nodo)
go

/* ============================================================ */
/*   Index: ord_nodo                                            */
/* ============================================================ */
create index ord_nodo on cms_nodos (ord_nodo)
go

/* ============================================================ */
/*   Table: cms_contenidos                                      */
/* ============================================================ */
create table cms_contenidos
(
    cod_contenido          numeric               identity,
    cod_user               numeric(10)           null    ,
    cod_nodo               numeric               null    ,
    cod_contenido_rel      numeric               null    ,
    cod_usuario_rel        numeric               null    ,
    titulo_contenido       varchar(500)          null    ,
    texto_contenido        text                  null    ,
    date_contenido         datetime              null    ,
    est_contenido          char(1)               null    ,
    prv_contendio          integer               null    ,
    dest_contenido         integer               null    ,
    ind_rss                char(1)               null    ,
    resumen_contenido      text                  null    ,
    timestamp              timestamp             null    ,
    ip_usuario             varchar(20)           null    ,
    ind_denuncia           char(1)               null    ,
    constraint PK_CMS_CONTENIDOS primary key (cod_contenido)
)
go

/* ============================================================ */
/*   Index: cod_usuario                                         */
/* ============================================================ */
create index cod_usuario on cms_contenidos (cod_user)
go

/* ============================================================ */
/*   Index: cod_nodo                                            */
/* ============================================================ */
create index cod_nodo on cms_contenidos (cod_nodo)
go

/* ============================================================ */
/*   Index: cod_contenido_rel                                   */
/* ============================================================ */
create index cod_contenido_rel on cms_contenidos (cod_contenido_rel)
go

/* ============================================================ */
/*   Index: cod_usuario_rel                                     */
/* ============================================================ */
create index cod_usuario_rel on cms_contenidos (cod_usuario_rel)
go

/* ============================================================ */
/*   Index: dest_contenido                                      */
/* ============================================================ */
create index dest_contenido on cms_contenidos (dest_contenido)
go

/* ============================================================ */
/*   Index: est_contenido                                       */
/* ============================================================ */
create index est_contenido on cms_contenidos (est_contenido)
go

/* ============================================================ */
/*   Index: prv_contendio                                       */
/* ============================================================ */
create index prv_contendio on cms_contenidos (prv_contendio)
go

/* ============================================================ */
/*   Index: ind_rss                                             */
/* ============================================================ */
create index ind_rss on cms_contenidos (ind_rss)
go

/* ============================================================ */
/*   Table: app_ranking                                         */
/* ============================================================ */
create table app_ranking
(
    cod_ranking            numeric               identity,
    cod_user               numeric(10)           null    ,
    cod_cliente            numeric               null    ,
    fch_ranking            datetime              null    ,
    nota_ranking           decimal               null    ,
    obs_ranking            text                  null    ,
    constraint PK_APP_RANKING primary key (cod_ranking)
)
go

/* ============================================================ */
/*   Table: sys_grupo_usuarios                                  */
/* ============================================================ */
create table sys_grupo_usuarios
(
    cod_user               numeric(10)           null    ,
    cod_grp                numeric(10)           null    
)
go

/* ============================================================ */
/*   Table: sys_perfiles_usuarios                               */
/* ============================================================ */
create table sys_perfiles_usuarios
(
    cod_user               numeric(10)           null    ,
    cod_perfil             numeric(10)           null    
)
go

/* ============================================================ */
/*   Table: cms_archivos                                        */
/* ============================================================ */
create table cms_archivos
(
    cod_archivo            numeric               identity,
    cod_contenido          numeric               null    ,
    cod_archivo_rel        numeric               null    ,
    nom_archivo            varchar(300)          null    ,
    des_archivo            text                  null    ,
    date_archivo           datetime              null    ,
    ext_archivo            varchar(10)           null    ,
    img_archivo            image                 null    ,
    timestamp              timestamp             null    ,
    constraint PK_CMS_ARCHIVOS primary key (cod_archivo)
)
go

/* ============================================================ */
/*   Index: cod_contenido                                       */
/* ============================================================ */
create index cod_contenido on cms_archivos (cod_contenido)
go

/* ============================================================ */
/*   Index: cod_archivo_rel                                     */
/* ============================================================ */
create index cod_archivo_rel on cms_archivos (cod_archivo_rel)
go

/* ============================================================ */
/*   Table: syr_info_usuarios                                   */
/* ============================================================ */
create table syr_info_usuarios
(
    cod_campo              numeric(25)           null    ,
    cod_user               numeric(10)           null    ,
    val_campo              varchar(255)          null    ,
    text_campo             text                  null    ,
    tip_campo              char(1)               null    
)
go

/* ============================================================ */
/*   Index: cod_campo                                           */
/* ============================================================ */
create index cod_campo on syr_info_usuarios (cod_campo)
go

/* ============================================================ */
/*   Index: cod_usuario                                         */
/* ============================================================ */
create index cod_usuario on syr_info_usuarios (cod_user)
go

/* ============================================================ */
/*   Index: tip_campo                                           */
/* ============================================================ */
create index tip_campo on syr_info_usuarios (tip_campo)
go

/* ============================================================ */
/*   Table: cmr_nodos_usuarios                                  */
/* ============================================================ */
create table cmr_nodos_usuarios
(
    cod_user               numeric(10)           null    ,
    cod_nodo               numeric               null    ,
    tipo                   char(1)               null    
)
go

/* ============================================================ */
/*   Index: cod_usuario                                         */
/* ============================================================ */
create index cod_usuario on cmr_nodos_usuarios (cod_user)
go

/* ============================================================ */
/*   Index: COD_NODO                                            */
/* ============================================================ */
create index COD_NODO on cmr_nodos_usuarios (cod_nodo)
go

/* ============================================================ */
/*   Index: tipo                                                */
/* ============================================================ */
create index tipo on cmr_nodos_usuarios (tipo)
go

/* ============================================================ */
/*   Table: cmr_nodos_grupos                                    */
/* ============================================================ */
create table cmr_nodos_grupos
(
    cod_grp                numeric(10)           null    ,
    cod_nodo               numeric               null    ,
    tipo                   char(1)               null    
)
go

/* ============================================================ */
/*   Index: cod_grupo                                           */
/* ============================================================ */
create index cod_grupo on cmr_nodos_grupos (cod_grp)
go

/* ============================================================ */
/*   Index: COD_NODO                                            */
/* ============================================================ */
create index COD_NODO on cmr_nodos_grupos (cod_nodo)
go

/* ============================================================ */
/*   Index: tipo                                                */
/* ============================================================ */
create index tipo on cmr_nodos_grupos (tipo)
go

/* ============================================================ */
/*   Table: cmr_contenido_usuario                               */
/* ============================================================ */
create table cmr_contenido_usuario
(
    cod_contenido          numeric               null    ,
    cod_user               numeric(10)           null    ,
    tipo                   char(1)               null    
)
go

/* ============================================================ */
/*   Index: cod_contenido                                       */
/* ============================================================ */
create index cod_contenido on cmr_contenido_usuario (cod_contenido)
go

/* ============================================================ */
/*   Index: cod_usuario                                         */
/* ============================================================ */
create index cod_usuario on cmr_contenido_usuario (cod_user)
go

/* ============================================================ */
/*   Index: tipo                                                */
/* ============================================================ */
create index tipo on cmr_contenido_usuario (tipo)
go

/* ============================================================ */
/*   Table: cmr_contenido_grupos                                */
/* ============================================================ */
create table cmr_contenido_grupos
(
    cod_contenido          numeric               null    ,
    cod_grp                numeric(10)           null    ,
    tipo                   char(1)               null    
)
go

/* ============================================================ */
/*   Index: cod_contenido                                       */
/* ============================================================ */
create index cod_contenido on cmr_contenido_grupos (cod_contenido)
go

/* ============================================================ */
/*   Index: cod_grupo                                           */
/* ============================================================ */
create index cod_grupo on cmr_contenido_grupos (cod_grp)
go

/* ============================================================ */
/*   Index: tipo                                                */
/* ============================================================ */
create index tipo on cmr_contenido_grupos (tipo)
go

/* ============================================================ */
/*   Table: sys_archivos_usuarios                               */
/* ============================================================ */
create table sys_archivos_usuarios
(
    cod_archivo            numeric               identity,
    cod_user               numeric(10)           null    ,
    nom_archivo            varchar(255)          null    ,
    date_archivo           datetime              null    ,
    tip_archivo            char(1)               null    ,
    img_archivo            image                 null    ,
    timestamp              timestamp             null    ,
    img_profile_archivo    varchar(255)          null    ,
    constraint PK_SYS_ARCHIVOS_USUARIOS primary key (cod_archivo)
)
go

/* ============================================================ */
/*   Index: cod_usuario                                         */
/* ============================================================ */
create index cod_usuario on sys_archivos_usuarios (cod_user)
go

/* ============================================================ */
/*   Index: nom_archivo                                         */
/* ============================================================ */
create index nom_archivo on sys_archivos_usuarios (nom_archivo)
go

/* ============================================================ */
/*   Index: tip_archivo                                         */
/* ============================================================ */
create index tip_archivo on sys_archivos_usuarios (tip_archivo)
go

/* ============================================================ */
/*   Table: sys_relacion_usuarios                               */
/* ============================================================ */
create table sys_relacion_usuarios
(
    cod_user               numeric(10)           null    ,
    cod_usuario_rel        numeric(10)           null    ,
    est_relacion           char(1)               null    
)
go

/* ============================================================ */
/*   Index: cod_usuario_rel                                     */
/* ============================================================ */
create index cod_usuario_rel on sys_relacion_usuarios (cod_usuario_rel)
go

/* ============================================================ */
/*   Index: est_relacion                                        */
/* ============================================================ */
create index est_relacion on sys_relacion_usuarios (est_relacion)
go

/* ============================================================ */
/*   Table: syr_campo_opciones                                  */
/* ============================================================ */
create table syr_campo_opciones
(
    cod_campo              numeric(25)           null    ,
    cod_opcion             numeric(25)           null    
)
go

/* ============================================================ */
/*   Table: sys_comentario_usuario                              */
/* ============================================================ */
create table sys_comentario_usuario
(
    cod_comentario         numeric               identity,
    cod_user               numeric(10)           null    ,
    cod_usuario_rel        numeric               null    ,
    comentario             text                  null    ,
    timestamp              timestamp             null    ,
    ip_usuario             varchar(20)           null    ,
    fec_usuario            datetime              null    ,
    constraint PK_SYS_COMENTARIO_USUARIO primary key (cod_comentario)
)
go

/* ============================================================ */
/*   Index: cod_usuario                                         */
/* ============================================================ */
create index cod_usuario on sys_comentario_usuario (cod_user)
go

/* ============================================================ */
/*   Index: cod_usuario_rel                                     */
/* ============================================================ */
create index cod_usuario_rel on sys_comentario_usuario (cod_usuario_rel)
go

/* ============================================================ */
/*   Table: cmr_cont_metadata                                   */
/* ============================================================ */
create table cmr_cont_metadata
(
    cod_contenido          numeric               null    ,
    cod_metadata           numeric               null    ,
    cod_valor_metadata     numeric               null    
)
go

/* ============================================================ */
/*   Index: cod_contenido                                       */
/* ============================================================ */
create index cod_contenido on cmr_cont_metadata (cod_contenido)
go

/* ============================================================ */
/*   Index: cod_metadata                                        */
/* ============================================================ */
create index cod_metadata on cmr_cont_metadata (cod_metadata)
go

/* ============================================================ */
/*   Index: cod_valor_metadata                                  */
/* ============================================================ */
create index cod_valor_metadata on cmr_cont_metadata (cod_valor_metadata)
go

/* ============================================================ */
/*   Table: cmr_metadata_valor                                  */
/* ============================================================ */
create table cmr_metadata_valor
(
    cod_metadata           numeric               null    ,
    cod_valor_metadaa      numeric               null    
)
go

/* ============================================================ */
/*   Index: cod_metadata                                        */
/* ============================================================ */
create index cod_metadata on cmr_metadata_valor (cod_metadata)
go

/* ============================================================ */
/*   Index: cod_valor_metadata                                  */
/* ============================================================ */
create index cod_valor_metadata on cmr_metadata_valor (cod_valor_metadaa)
go

/* ============================================================ */
/*   Table: sys_seguir_usuarios                                 */
/* ============================================================ */
create table sys_seguir_usuarios
(
    cod_user               numeric(10)           null    ,
    cod_seg_usuario        numeric(10)           null    
)
go

/* ============================================================ */
/*   Table: sys_emailtouser                                     */
/* ============================================================ */
create table sys_emailtouser
(
    cod_email              numeric               identity,
    cod_email_rel          numeric               null    ,
    cod_user               numeric(10)           null    ,
    cod_usr_send_email     numeric               null    ,
    cuerpo_email           text                  null    ,
    fecha_email            datetime              null    ,
    est_email              char(1)               null    ,
    constraint PK_SYS_EMAILTOUSER primary key (cod_email)
)
go

/* ============================================================ */
/*   Table: apt_preg_ranking                                    */
/* ============================================================ */
create table apt_preg_ranking
(
    cod_ranking            numeric               null    ,
    cod_preg_ranking       numeric               null    ,
    nota_preg_ranking      int                   null    
)
go

/* ============================================================ */
/*   Table: app_img_banner                                      */
/* ============================================================ */
create table app_img_banner
(
    cod_img_banner         numeric               identity,
    cod_banner             numeric               null    ,
    nom_img_banner         varchar(255)          null    ,
    text_img_banner        text                  null    ,
    ord_img_banner         int                   null    ,
    url_img_banner         varchar(255)          null    ,
    constraint PK_APP_IMG_BANNER primary key (cod_img_banner)
)
go

alter table sys_usuario
    add constraint FK_SYS_USUA_REF_5215_SYS_TIPO foreign key  (cod_tipo)
       references sys_tipo_usuario (cod_tipo)
go

alter table cms_nodos
    add constraint FK_CMS_NODO_REF_5314_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table cms_nodos
    add constraint FK_CMS_NODO_REF_5328_CMS_TEMP foreign key  (cod_template)
       references cms_template (cod_template)
go

alter table cms_contenidos
    add constraint FK_CMS_CONT_REF_5318_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table cms_contenidos
    add constraint FK_CMS_CONT_REF_5322_CMS_NODO foreign key  (cod_nodo)
       references cms_nodos (cod_nodo)
go

alter table app_ranking
    add constraint FK_APP_RANK_REF_10014_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table sys_grupo_usuarios
    add constraint FK_SYS_GRUP_REF_5232_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table sys_grupo_usuarios
    add constraint FK_SYS_GRUP_REF_5239_SYS_GRUP foreign key  (cod_grp)
       references sys_grupos (cod_grp)
go

alter table sys_perfiles_usuarios
    add constraint FK_SYS_PERF_REF_5220_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table sys_perfiles_usuarios
    add constraint FK_SYS_PERF_REF_5244_SYS_PERF foreign key  (cod_perfil)
       references sys_perfiles (cod_perfil)
go

alter table cms_archivos
    add constraint FK_CMS_ARCH_REF_5264_CMS_CONT foreign key  (cod_contenido)
       references cms_contenidos (cod_contenido)
go

alter table syr_info_usuarios
    add constraint FK_SYR_INFO_REF_1853_SYS_CAMP foreign key  (cod_campo)
       references sys_campo_usuarios (cod_campo)
go

alter table syr_info_usuarios
    add constraint FK_SYR_INFO_REF_5199_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table cmr_nodos_usuarios
    add constraint FK_CMR_NODO_REF_5293_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table cmr_nodos_usuarios
    add constraint FK_CMR_NODO_REF_5302_CMS_NODO foreign key  (cod_nodo)
       references cms_nodos (cod_nodo)
go

alter table cmr_nodos_grupos
    add constraint FK_CMR_NODO_REF_5285_SYS_GRUP foreign key  (cod_grp)
       references sys_grupos (cod_grp)
go

alter table cmr_nodos_grupos
    add constraint FK_CMR_NODO_REF_5306_CMS_NODO foreign key  (cod_nodo)
       references cms_nodos (cod_nodo)
go

alter table cmr_contenido_usuario
    add constraint FK_CMR_CONT_REF_5273_CMS_CONT foreign key  (cod_contenido)
       references cms_contenidos (cod_contenido)
go

alter table cmr_contenido_usuario
    add constraint FK_CMR_CONT_REF_5277_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table cmr_contenido_grupos
    add constraint FK_CMR_CONT_REF_5269_CMS_CONT foreign key  (cod_contenido)
       references cms_contenidos (cod_contenido)
go

alter table cmr_contenido_grupos
    add constraint FK_CMR_CONT_REF_5281_SYS_GRUP foreign key  (cod_grp)
       references sys_grupos (cod_grp)
go

alter table sys_archivos_usuarios
    add constraint FK_SYS_ARCH_REF_5207_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table sys_relacion_usuarios
    add constraint FK_SYS_RELA_REF_5211_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table syr_campo_opciones
    add constraint FK_SYR_CAMP_REF_2539_SYS_CAMP foreign key  (cod_campo)
       references sys_campo_usuarios (cod_campo)
go

alter table syr_campo_opciones
    add constraint FK_SYR_CAMP_REF_2543_SYS_OPCI foreign key  (cod_opcion)
       references sys_opciones_campo (cod_opcion)
go

alter table sys_comentario_usuario
    add constraint FK_SYS_COME_REF_5203_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table cmr_cont_metadata
    add constraint FK_CMR_CONT_REF_5260_CMS_CONT foreign key  (cod_contenido)
       references cms_contenidos (cod_contenido)
go

alter table cmr_metadata_valor
    add constraint FK_CMR_META_REF_5252_CMS_VALO foreign key  (cod_valor_metadaa)
       references cms_valor_metadata (cod_valor_metadaa)
go

alter table cmr_metadata_valor
    add constraint FK_CMR_META_REF_5256_CMS_META foreign key  (cod_metadata)
       references cms_metadata (cod_metadata)
go

alter table sys_seguir_usuarios
    add constraint FK_SYS_SEGU_REF_5877_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table sys_emailtouser
    add constraint FK_SYS_EMAI_REF_7020_SYS_USUA foreign key  (cod_user)
       references sys_usuario (cod_user)
go

alter table apt_preg_ranking
    add constraint FK_APT_PREG_REF_10027_APP_RANK foreign key  (cod_ranking)
       references app_ranking (cod_ranking)
go

alter table apt_preg_ranking
    add constraint FK_APT_PREG_REF_10031_APP_PREG foreign key  (cod_preg_ranking)
       references app_preg_ranking (cod_preg_ranking)
go

alter table app_img_banner
    add constraint FK_APP_IMG__REF_12248_APP_BANN foreign key  (cod_banner)
       references app_banner (cod_banner)
go

