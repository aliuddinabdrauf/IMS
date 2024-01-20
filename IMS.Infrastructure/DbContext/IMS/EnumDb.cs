namespace IMS.Infrastructure.DbContext.IMS;


public enum UserRole
{
    User = 1,
    Admin = 2
}

public enum AccountType
{
    All = 0,
    Student = 1,
    Staff = 2,
    Industry = 3
}

public enum UserStatus
{
    NeedActivation = 1,
    Active = 2,
    Disabled = 3,
    Deleted = 4
}

public enum UserGender
{
    Male = 1,
    Female = 2
}