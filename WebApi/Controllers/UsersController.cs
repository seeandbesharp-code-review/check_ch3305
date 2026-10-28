using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Zxcvbn;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //fix: A controller class name ends with Controller. The route stays api/Users
    //fix: The file is renamed to UsersController.cs, so it matches the class
    public class UsersController : ControllerBase
    {
        // GET: api/<Users>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        //fix: The path never changes, so it can be readonly
        static readonly string filePath = "text.txt";

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    //fix: text.txt starts with an empty line, and an empty line is not JSON (it crashed with 500). Skip it, like you do in Login
                    if (string.IsNullOrWhiteSpace(currentUserInFile))
                    {
                        continue;
                    }
                    User user =JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.id == id)
                        return Ok(user);
                }
            }
            return NotFound(); 

        }

        // POST api/<Users>
        [HttpPost]
        //Parameters are written in camelCase: postUser (also in Login)
        public ActionResult<User> Post([FromBody] User PostUser)
        {
            int numberOfUsers = System.IO.File.ReadLines(filePath).Count();
            PostUser.id = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(PostUser);
            System.IO.File.AppendAllText(filePath, userJson +
            Environment.NewLine);
            return CreatedAtAction(nameof(Get), new { id = PostUser.id }, PostUser);
        }
        //The assignment asks a wrong login to return NoContent() (204)
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
        //The assignment asks PUT to return Ok(): return Ok() at the end instead of NoContent()
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
                    //fix: Skip the empty line (it crashed with 500)
                    if (string.IsNullOrWhiteSpace(currentUserInFile))
                    {
                        continue;
                    }
                    User user1 = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user1.id == id)
                    {
                        textToReplace = currentUserInFile;
                        //fix: Found- break
                        break;
                    }
                }
            }
                if(textToReplace != string.Empty)
               {
                //fix: Keep the id, otherwise the updated user is saved without it
                user.id = id;
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
