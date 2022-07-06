using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EstudiantesCore1.Dtos;
using EstudiantesCore1.Entidades;
using EstudiantesCore1.interactores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace GestionEstudiante1.Pages
{
    public class EstudiantesModel : PageModel
    {

        private readonly IMatricula _matricula;
        public void OnGet()
        {
        }

        public EstudiantesModel(IMatricula matricula)
        {
            _matricula = matricula;
        }


        [HttpPost]
        public IActionResult OnPostCrearEstudiante(Estudiante estudiante)
        {
            try
            {
                bool modelValido= TryValidateModel(estudiante);
                if (modelValido)
                {
                    if (estudiante.id > 0)
                    {
                        _matricula.ActualizarEstudiante(estudiante);
                    }
                    else
                    {
                        _matricula.MatricularEstudiante(estudiante);
                    }                    
                }
                else
                {
                    return StatusCode(400, "modelo inavlido");
                }
                //_matricula.MatricularEstudiante();
                return StatusCode(200, "todo bien");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        /// <summary>
        /// valida que el documento de identidad no se repita
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OnGetVerificarIdentificacion(int idtipodocumento, string documento)
        {
            try
            {
                if (!string.IsNullOrEmpty(documento)&& idtipodocumento>0)
                {
                    bool existe = !_matricula.VerificarEstudianteByDocumento(idtipodocumento, documento);

                    return StatusCode(200, existe);
                }
                
                return StatusCode(200, true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult OnGetTipoDocumento(DataSourceLoadOptions options)
        {
            try {
                List<TipoDocumento> documentos = _matricula.GetDocumentos();
                return new JsonResult(DataSourceLoader.Load(documentos, options));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpGet]
        public IActionResult OnGetEstados(DataSourceLoadOptions options)
        {
            try
            {
                List<EstadoEstudiante> estados = _matricula.GetEstadoEstudiantes();
                return new JsonResult(DataSourceLoader.Load(estados, options));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpGet]
        public IActionResult OnGetObtenerEstudiantes(DataSourceLoadOptions options)
        {
            try
            {
                List<Estudiante> estudiantes = _matricula.ObtenerTodosEstudiantes();
                return new JsonResult(DataSourceLoader.Load(estudiantes, options));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpGet]
        public IActionResult OnGetObtenerEstudiante(int idEstudiante)
        {
            try
            {
                if(idEstudiante > 0)
                {
                    Estudiante estudiante = _matricula.ObtenerEstudiante(idEstudiante);
                    return StatusCode(200, estudiante);
                }
                else
                {
                    return StatusCode(200,null);
                }
                
            }catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
