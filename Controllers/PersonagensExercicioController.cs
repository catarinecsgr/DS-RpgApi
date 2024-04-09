using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [Route("[controller]")]
    public class PersonagensExercicioController : Controller
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            //Colar os objetos da lista do chat aqui
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };
        
        [HttpGet("GetByName/{nome}")]
        public IActionResult GetByName(string nome) 
        {
            List<Personagem> novaLista = personagens.FindAll(p => p.Nome.Contains(nome));
            if(novaLista.Count < 1 || nome == null) 
            {
                return NotFound("Nenhum personagem foi encontrado.");
            }
            return Ok(novaLista);
        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Personagem novoPersonagem)
        {
            if (novoPersonagem.Defesa < 10)
                return BadRequest("O atributo de defesa não pode ser menor que 10.");

            if (novoPersonagem.Inteligencia > 30)
                return BadRequest("O atributo de inteligência não pode ser maior que 30.");

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem novoPersonagem)
        {
            if (novoPersonagem.Classe == ClasseEnum.Mago && novoPersonagem.Inteligencia < 35)
                return BadRequest("O personagem de classe mago não pode ter o atributo inteligência menor que 35");

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            List<Personagem> novaLista = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro);
            novaLista.OrderByDescending(per => per.PontosVida).ToList();
            return Ok(novaLista);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            int quantidade = personagens.Count();
            int soma = personagens.Sum(p => p.Inteligencia);
            return Ok($"Quantidade: {quantidade}. Soma de inteligencia dos personagens: {soma}");
        }

        [HttpGet("GetByClasse/{classe}")]
        public IActionResult GetByClasse(ClasseEnum classe)
        {
            List<Personagem> novaLista = personagens.FindAll(p => p.Classe == classe);
            return Ok(novaLista);
        }


    }
}