using EstudiantesCore1.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace GestionEstudiante1.Pages
{
    public class EstudiantesModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult onPostCrearEstudiante(EstudianteDTO estudiante)
        {
            try
            {
                return StatusCode(200, "todo bien");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
