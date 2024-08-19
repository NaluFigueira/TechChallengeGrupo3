namespace PosTech.TechChallenge.Contacts.Application;

public static class PhoneNumberValidations
{

    public static bool BeAValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.Length != 9 || (phoneNumber.Length == 9 && phoneNumber.StartsWith('9'));
    }
}
