using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EstudiantesCore1.Entidades;
using EstudiantesCore1.interactores;
using EstudiantesCore1.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GestionEstudiante1.Pages
{
    public class ProfesoresModel : PageModel
    {
        private readonly IGestionProfesores _matricula;

        public void OnGet()
        {
        }

        public ProfesoresModel(IGestionProfesores matricula)
        {
            _matricula = matricula;
        }

        [HttpGet]
        public IActionResult OnGetObtenerProfesores(DataSourceLoadOptions options)
        {
            try
            {
                List<Profesor> profesores = _matricula.ObtenerTodosProfesores();
                return new JsonResult(DataSourceLoader.Load(profesores, options));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        [HttpPost]
        public IActionResult OnPostCrearProfesor(string values)
        {
            try
            {
                Profesor profesor = new Profesor();
                profesor = JsonConvert.DeserializeObject<Profesor>(values);
                _matricula.CrearNuevoProfesor(profesor);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return BadRequest("No se pudo agregar el profesor");
            }
        }
        [HttpGet]
        public IActionResult OnGetObtenerTipoDeDocumento(DataSourceLoadOptions options)
        {
            try
            {
                List<TipoDocumento> documentos = _matricula.GetDocumentos();
                return new JsonResult(DataSourceLoader.Load(documentos, options));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult OnGetObtenerEstadoProfesor(DataSourceLoadOptions options)
        {
            try
            {
                List<EstadoProfesor> estadoProfesores = _matricula.getEstadoProfesores();
                return new JsonResult(DataSourceLoader.Load(estadoProfesores, options));

            }
            catch (Exception e)
            {
                return BadRequest("Existio un error al cargar los datos");
            }
        }
        [HttpGet]
        public IActionResult OnGetVerificarIdentificacion(int idtipodocumento, string documento)
        {
            try
            {
                if (!string.IsNullOrEmpty(documento) && idtipodocumento > 0)
                {
                    bool existe = !_matricula.VerificarProfesorByDocumento(idtipodocumento, documento);

                    return StatusCode(200, existe);
                }

                return StatusCode(200, true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
