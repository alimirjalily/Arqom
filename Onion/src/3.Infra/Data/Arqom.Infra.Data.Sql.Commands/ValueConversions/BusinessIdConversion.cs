using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Arqom.Core.Domain.ValueObjects;

namespace Arqom.Infra.Data.Sql.Commands.ValueConversions
{
    public class BusinessIdConversion : ValueConverter<BusinessId, Guid>
    {
        public BusinessIdConversion() : base(c => c.Value, c => BusinessId.FromGuid(c))
        {

        }
    }
}
