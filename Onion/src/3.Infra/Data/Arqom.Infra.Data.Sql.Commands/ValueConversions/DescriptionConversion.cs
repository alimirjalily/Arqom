using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Arqom.Core.Domain.Toolkits.ValueObjects;

namespace Arqom.Infra.Data.Sql.Commands.ValueConversions
{
    public class DescriptionConversion : ValueConverter<Description, string>
    {
        public DescriptionConversion() : base(c => c.Value, c => Description.FromString(c))
        {

        }
    }
}
