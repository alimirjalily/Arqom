using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Arqom.Core.Domain.Toolkits.ValueObjects;

namespace Arqom.Infra.Data.Sql.Commands.ValueConversions
{
    public class NationalCodeConversion : ValueConverter<NationalCode, string>
    {
        public NationalCodeConversion() : base(c => c.Value, c => NationalCode.FromString(c))
        {

        }
    }
}
