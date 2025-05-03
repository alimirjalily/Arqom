using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Arqom.Core.Domain.Toolkits.ValueObjects;

namespace Arqom.Infra.Data.Sql.Commands.ValueConversions
{
    public class TitleConversion : ValueConverter<Title, string>
    {
        public TitleConversion() : base(c => c.Value, c => Title.FromString(c))
        {

        }
    }
}
