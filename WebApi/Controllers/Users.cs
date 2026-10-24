using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Zxcvbn;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        // GET: api/<Users>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        static string filePath = "text.txt";

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user =JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.id == id)
                        return Ok(user);
                }
            }
            return NotFound(); 

        }

        // POST api/<Users>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User PostUser)
        {
            int numberOfUsers = System.IO.File.ReadLines(filePath).Count();
            PostUser.id = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(PostUser);
            System.IO.File.AppendAllText(filePath, userJson +
            Environment.NewLine);
            return CreatedAtAction(nameof(Get), new { id = PostUser.id }, PostUser);
        }
        [HttpPost("Login")]
        public ActionResult<User> Post([FromBody] LoginUser PostUser)
        {
            
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); 
            }

            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    
                    if (string.IsNullOrWhiteSpace(currentUserInFile))
                    {
                        continue;
                    }

                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);

                    if (user != null && user.email == PostUser.email && user.password == PostUser.password)
                    {
                        return Ok(user);
                    }
                }
            }
            return NotFound();
        }
        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            if (user == null)
                return BadRequest();
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user1 = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user1.id == id)
                        textToReplace = currentUserInFile;
                }
            }
                if(textToReplace != string.Empty)
               {
                string text = System.IO.File.ReadAllText(filePath);
                text = text.Replace(textToReplace,
                JsonSerializer.Serialize(user));
                System.IO.File.WriteAllText(filePath, text);
               }
            return NoContent();
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
