namespace Domain.ValueObjects;

public enum CustomerClass
{
    Unkonw = 0,
    Hotel = 1,
    Buyer = 2,
    SubAcquirer = 3,
}

public static class CustomerClassParse
{
    public static CustomerClass GetCustomerClassValue(string customerClassValue)
    {
        if (!String.IsNullOrWhiteSpace(customerClassValue))
        {
            if (customerClassValue?.ToLower() == "unkonw")
                return CustomerClass.Unkonw;
            else if (customerClassValue?.ToLower() == "hotel")
                return CustomerClass.Hotel;
            else if (customerClassValue?.ToLower() == "buyer")
                return CustomerClass.Buyer;
            else if (customerClassValue?.ToLower() == "subacquirer")
                return CustomerClass.SubAcquirer;
            else
                throw new PublicException(
                    $"{customerClassValue} is an invalid CustomerClass, try: 'Unkonw', 'Hotel', 'Buyer', 'SubAcquirer' "
                );
        }
        else
        {
            throw new PublicException($"CustomerClass can not be null or empty ");
        }
    }

    public static CustomerClass GetCustomerClassValue(int customerClassValue)
    {
        if (customerClassValue == 0)
            return CustomerClass.Unkonw;
        else if (customerClassValue == 1)
            return CustomerClass.Hotel;
        else if (customerClassValue == 2)
            return CustomerClass.Buyer;
        else if (customerClassValue == 3)
            return CustomerClass.SubAcquirer;
        else
            throw new PublicException($"{customerClassValue} is an invalid CustomerClass");
    }
}
