namespace DumpApplication.WebApi.Models.Entities
{
    public class EmployeeData
    {
       /* Property 1 */
        public   /* Guid is a datatype , which produces unique values , so there will be no collision -> And it doesnt depend on Database (usually id is increamented by and in database only !)*/  
        Guid Id { get; set; }    // identifier na declared variable is called identifier (for example -> int A -> here A is identifier )

        /* Property 2 */
        public required string Name { get; set; }

        /* Property 3 */
        public required string Email { get; set; } 

        /* Property 4 */
        public string? Phone { get; set; }

        /* Property 5 */ //-> so everything we declare here in the model is Property !!
        public decimal PhoneNumber { get; set; }
    }
}
