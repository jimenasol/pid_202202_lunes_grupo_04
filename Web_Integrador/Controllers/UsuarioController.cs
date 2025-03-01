﻿using Entidades;
using Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Integrador.Model;
using Web_Integrador.Security;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        Usuario_BS ub = new Usuario_BS();
        Perfil_BS pb = new Perfil_BS();
        Tipo_BS tb = new Tipo_BS();
        Rol_BS rb = new Rol_BS();
        // GET: Usuario
        [PermisosRol(Roles.Administrador)]
        public ActionResult Index()
        {

            var dataDocumento = tb.Listar_TipoUtil("documento");
            var dataGenero = tb.Listar_TipoUtil("genero");
            var datanacionalidad = tb.Listar_TipoUtil("nacionalidad");

            ViewBag.lstgenero = dataGenero.TipoList.ToList();
            ViewBag.lstdocumento = dataDocumento.TipoList.ToList();
            ViewBag.lstnacionalidad = datanacionalidad.TipoList.ToList();

            return View();
        }

        public ActionResult SetRolMantenimiento()
        {
            var dataRoles = rb.lista(0);

            ViewBag.lstRoles = dataRoles.RolList;

            return View();
        }

        public JsonResult ListarUsuarios(int id = 0)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            try
            {
                var rpta = ub.lista(id);
                ur = rpta;
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            
            return Json(ur, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPerfil(int id = 0)
        {
            Perfil_res pr = new Perfil_res();
            try
            {
                var rpta = pb.Listar_Perfil_Usuario(id);
                pr = rpta;
            }
            catch (Exception ex)
            {
                pr.oHeader.estado = false;
                pr.oHeader.mensaje = ex.Message;
            }
            return Json(pr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearUsuario(Usuario user)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            List<UsuarioGeneral> ulist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = ub.Registrar_Usuario(user);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getUsuario = ub.lista(rpta.id_register);
                    if (getUsuario.oHeader.estado)
                    {
                        ulist = getUsuario.UsuarioList;
                    }
                }
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            ur.oHeader = oHeader;
            ur.UsuarioList = ulist;


            return Json(ur, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarPerfil(Perfil p)
        {
            Perfil_res_UsG ur = new Perfil_res_UsG();
            List<UsuarioGeneral> plist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = pb.Editar_MiPerfil(p);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getPerfil = ub.lista(rpta.id_register);
                    if (getPerfil.oHeader.estado)
                    {
                        plist = getPerfil.UsuarioList;
                    }
                }
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            ur.oHeader = oHeader;
            ur.ListaUsuarioP = plist;


            return Json(ur, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CambiarEstado(Usuario u)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            List<UsuarioGeneral> ulist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = ub.CambiarEstado_Us(u);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getUsuario = ub.lista(rpta.id_register);
                    if (getUsuario.oHeader.estado)
                    {
                        ulist = getUsuario.UsuarioList;
                    }
                }
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            ur.oHeader = oHeader;
            ur.UsuarioList = ulist;


            return Json(ur, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CambiarContraseña(Usuario u)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            List<UsuarioGeneral> ulist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = ub.CambiarContraseña_Us(u);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getUsuario = ub.lista(rpta.id_register);
                    if (getUsuario.oHeader.estado)
                    {
                        ulist = getUsuario.UsuarioList;
                    }
                }
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            ur.oHeader = oHeader;
            ur.UsuarioList = ulist;
            return Json(ur, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CambiarRolSet(SetRolUser u)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            List<UsuarioGeneral> ulist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = ub.CambiarRolUsuario(u);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getUsuario = ub.lista(rpta.id_register);
                    if (getUsuario.oHeader.estado)
                    {
                        ulist = getUsuario.UsuarioList;
                    }
                }
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            ur.oHeader = oHeader;
            ur.UsuarioList = ulist;
            return Json(ur, JsonRequestBehavior.AllowGet);
        }



    }
}