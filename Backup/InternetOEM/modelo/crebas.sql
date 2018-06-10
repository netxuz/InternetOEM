/* ============================================================ */
/*   Database name:  "MBD SOA"                                  */
/*   DBMS name:      Microsoft SQL Server 7.x                   */
/*   Created on:     16/11/2012  12:03                          */
/* ============================================================ */

/* ============================================================ */
/*   Table: tbl_usuarios                                        */
/* ============================================================ */
create table tbl_usuarios
(
    id_usuario          numeric               not null,
    nom_usuario         varchar(255)          null    ,
    uid_ldap_usuario    varhcar(50)           null    ,
    eml_usuario         varchar(50)           null    ,
    est_usuario         char(1)               null    ,
    constraint PK_TBL_USUARIOS primary key (id_usuario)
)
go

/* ============================================================ */
/*   Table: tbl_roles_sharepoint                                */
/* ============================================================ */
create table tbl_roles_sharepoint
(
    id_usuario          numeric               null    ,
    rol_sharepoint      varchar(100)          null    ,
    est_rol_sharepoint  char(1)               null    
)
go

alter table tbl_roles_sharepoint
    add constraint FK_TBL_ROLE_REF_33_TBL_USUA foreign key  (id_usuario)
       references tbl_usuarios (id_usuario)
go

