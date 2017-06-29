using Tnf.Provider.Carol.Mappers;

namespace Tnf.Architecture.Data.Entities
{
    public class PresidentPocoMapper : CarolMapper<PresidentPoco>
    {
        public PresidentPocoMapper()
        {
            StagingTypeName("Test_Entity");

            LoadSchemaFromObject(new
            {
                properties = new
                {
                    name = "string",
                    address = new
                    {
                        properties = new
                        {
                            street = "string",
                            number = "string",
                            complement = "string",
                            zipCode = new
                            {
                                properties = new
                                {
                                    number = "string"
                                },
                                type = "nested"
                            }
                        },
                        type = "nested"
                    }
                }
            });
        }
    }
}
