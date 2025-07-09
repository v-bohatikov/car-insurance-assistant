using System.Data;
using SharedKernel.Results;

namespace SharedKernel.Extensions;

public static class EnumExtensions
{
    public static Result<TResultEnum> MapSemantically<TOriginEnum, TResultEnum>(this TOriginEnum originEnum)
        where TOriginEnum : struct, IConvertible
        where TResultEnum : struct, IConvertible
    {
        var originType = typeof(TOriginEnum);
        var resultType = typeof(TResultEnum);
        if (!originType.IsEnum || !resultType.IsEnum)
        {
            throw new ConstraintException(
                "Both types should be enums");
        }

        var resultEnum = Enum.Parse<TResultEnum>(originEnum.ToString()!, true);
        return Result.Success(resultEnum);
    }
}