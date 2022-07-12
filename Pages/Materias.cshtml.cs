using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EstudiantesCore1.Entidades;
using EstudiantesCore1.interactores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GestionEstudiante1.Pages
{
    public class MateriasModel : PageModel
    {
        private readonly IMatricula _matricula;

        public MateriasModel(IMatricula matricula)
        {
            _matricula = matricula;
        }

            public void OnGet()
        {
        }

        [HttpGet]
        public IActionResult OnGetObtenerMaterias(DataSourceLoadOptions options)
        {
            try
            {
                List<Materia> materias = _matricula.GetMaterias();
                return new JsonResult(DataSourceLoader.Load(materias,options));

            }catch(Exception e)
            {
                return BadRequest("Existio un error al cargar los datos");
            }
        }

        [HttpGet]
        public IActionResult OnGetObtenerEstadoMaterias(DataSourceLoadOptions options)
        {
            try
            {
                List<EstadoMateriaEstudiante> estadoMaterias = _matricula.GetEstadoMaterias();
                return new JsonResult(DataSourceLoader.Load(estadoMaterias, options));

            }
            catch (Exception e)
            {
                return BadRequest("Existio un error al cargar los datos");
            }
        }

        [HttpGet]
        public IActionResult OnGetVerificarCodigoUnico(string materiaCode)
        {
            try
            {
                bool existe = true;
                if (!string.IsNullOrEmpty(materiaCode))
                {
                    existe=!_matricula.VerificarCodigoUnicoMateria(materiaCode);
                }
                return StatusCode(200, existe);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        [HttpPost]
        public IActionResult OnPostCrearMateria(string values)
        {
            try
            {
                Materia materia = new Materia();
                materia = JsonConvert.DeserializeObject<Materia>(values);
                _matricula.CrearNuevaMateria(materia);
                return StatusCode(200);

            }catch(Exception e)
            {
                return BadRequest("No se pudo agregar la materia");
            }
        }

        [HttpPut]
        public IActionResult OnPutActualizarMateria(int key, string values)
        {
            try
            {
                if (key != 0)
                {
                    Materia materiaExistente = _matricula.GetMateriaById(key);
                    JsonConvert.PopulateObject(values, materiaExistente);
                    _matricula.ActualizarMateria(materiaExistente);
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(400,"No se encontro la materia");
                }
                

            }
            catch (Exception e)
            {
                return BadRequest("No se pudo agregar la materia");
            }
        }
    }
}
