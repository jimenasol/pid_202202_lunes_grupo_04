﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;
using Helpers;
using Web_Integrador.Security;


namespace Web_Integrador.Controllers
{
    [Authorize]
    public class VisitanteController : Controller
    {
        Visitante_BS visitante_BS = new Visitante_BS();
        Tipo_BS tipo_BS = new Tipo_BS();
        Sucursal_BS sucursal_BS = new Sucursal_BS();
        Torre_BS torre_BS = new Torre_BS();
        Sector_BS sector_BS = new Sector_BS();
        Departamento_BS departamento_BS = new Departamento_BS();

        // GET: Sucursal
        [PermisosRol(Roles.AgenteVisitas)]

        public ActionResult Index()
        {
            var tiposView = tipo_BS.lista_Tipos(0);
            var cboSurcusal = sucursal_BS.lista(0);

            ViewBag.cboSurcusalvb = cboSurcusal.SucursalList;
            var listaTipos = tiposView.TipoList;
            return View(listaTipos);
        }
        public JsonResult ListarVisitantes(int id_visitante = 0)
        {
            Visitante_Res visitante_Res = new Visitante_Res();
            try
            {
                var rpta = visitante_BS.lista(id_visitante);
                visitante_Res = rpta;
            }
            catch (Exception ex)
            {
                visitante_Res.oHeader.estado = false;
                visitante_Res.oHeader.mensaje = ex.Message;
            }
            return Json(visitante_Res, JsonRequestBehavior.AllowGet);
        }
        //FILTRO POR DNI
        public JsonResult ListarVisitantesPorDni(string nro_documento = "")
        {
            Visitante_Res visitante_Res = new Visitante_Res();
            try
            {
                var rpta = visitante_BS.listaXDni(nro_documento);
                visitante_Res = rpta;
            }
            catch (Exception ex)
            {
                visitante_Res.oHeader.estado = false;
                visitante_Res.oHeader.mensaje = ex.Message;
            }
            return Json(visitante_Res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarVisitante(Visitante visi)
        {
            Visitante_Res visitante_Res = new Visitante_Res();
            List<Visitante> vicitante_List = new List<Visitante>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = visitante_BS.Registrar(visi);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getVisitante = visitante_BS.lista(rpta.id_register);
                    if (getVisitante.oHeader.estado)
                    {
                        vicitante_List = getVisitante.VisitanteList;
                    }
                }
            }
            catch (Exception ex)
            {
                visitante_Res.oHeader.estado = false;
                visitante_Res.oHeader.mensaje = ex.Message;
            }
            visitante_Res.oHeader = oHeader;
            visitante_Res.VisitanteList = vicitante_List;


            return Json(visitante_Res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarSectores(int id_sector = 0)
        {
            Sector_Suc_Res sector_res = new Sector_Suc_Res();
            try
            {
                var rpta = sector_BS.listar_suc(id_sector);
                sector_res = rpta;
            }
            catch (Exception ex)
            {
                sector_res.oHeader.estado = false;
                sector_res.oHeader.mensaje = ex.Message;
            }
            return Json(sector_res, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarDepartamentos(int id_departamento = 0)
        {
            var departamento = departamento_BS.lista(id_departamento);
            return Json(departamento, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarTorre(int id_sector = 0)
        {
            Torre_Res torre_Res = new Torre_Res();
            try
            {
                var rpta = torre_BS.Listar(0);
                if (rpta.oHeader.estado)
                {
                    var lista = rpta.TorreList;
                    var listaTorre = lista.Where(x => x.id_sector == id_sector).ToList();
                    torre_Res.oHeader = rpta.oHeader;
                    torre_Res.TorreList = listaTorre;
                }
                else
                {
                    torre_Res = rpta;
                }
            }
            catch (Exception ex)
            {
                torre_Res.oHeader.estado = false;
                torre_Res.oHeader.mensaje = ex.Message;
            }

            return Json(torre_Res, JsonRequestBehavior.AllowGet);

        }


        public JsonResult RegistrarEntrada(VISITA_REGISTRO data)
        {
            VISITAREGISTROEntrada_Res visreg_Res = new VISITAREGISTROEntrada_Res();
            List<Visitante> visreg_List = new List<Visitante>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = visitante_BS.RegistrarEntradaUsuario(data);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getVisitaReg = visitante_BS.lista(rpta.id_register);
                    if (getVisitaReg.oHeader.estado)
                    {
                        visreg_List = getVisitaReg.VisitanteList;
                    }
                }
            }
            catch (Exception ex)
            {
                visreg_Res.oHeader.estado = false;
                visreg_Res.oHeader.mensaje = ex.Message;
            }
            visreg_Res.oHeader = oHeader;
            visreg_Res.VisitaRegistroList = visreg_List;


            return Json(visreg_Res, JsonRequestBehavior.AllowGet);
        }
    }
}