namespace Domain.ValueObjects;

public enum CustomerType
{
    Unkonw = 0,
    Group = 1,
    Network = 2,
    Partner = 3,
    Hotel = 4
}

public static class CustomerTypeParse
{
    public static CustomerType GetCustomerTypeValue(string customerTypeValue)
    {
        if (!String.IsNullOrWhiteSpace(customerTypeValue))
        {
            if (customerTypeValue?.ToLower() == "unkonw")
                return CustomerType.Unkonw;
            else if (customerTypeValue?.ToLower() == "group")
                return CustomerType.Group;
            else if (customerTypeValue?.ToLower() == "network")
                return CustomerType.Network;
            else if (customerTypeValue?.ToLower() == "partner")
                return CustomerType.Partner;
            else if (customerTypeValue?.ToLower() == "hotel")
                return CustomerType.Hotel;
            else
                throw new PublicException(
                    $"{customerTypeValue} is an invalid CustomerType, try: 'Unkonw', 'Group', 'Network', 'Partner', 'Hotel' "
                );
        }
        else
        {
            throw new PublicException($"CustomerType can not be null or empty ");
        }
    }

    public static CustomerType GetCustomerTypeValue(int customerTypeValue)
    {
        if (customerTypeValue == 0)
            return CustomerType.Unkonw;
        else if (customerTypeValue == 1)
            return CustomerType.Group;
        else if (customerTypeValue == 2)
            return CustomerType.Network;
        else if (customerTypeValue == 3)
            return CustomerType.Partner;
        else if (customerTypeValue == 4)
            return CustomerType.Hotel;
        else
            throw new PublicException($"{customerTypeValue} is an invalid CustomerType");
    }
}
