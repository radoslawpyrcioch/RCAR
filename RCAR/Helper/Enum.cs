using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Extension
{
    public enum IdMessage
    {
        AccountLock,
        SendConfirmEmailSucces,
        SendConfirmEmailError,
        EmailConfirmedSucces,
        EmailConfirmedError,
        ResetPasswordTokenSendSucces,
        ResetPasswordTokenSendError,
        ResetPasswordSucces,
        ChangePasswordSucces,
        AddPhoneNumberSucces,
        AddPhoneNumberError,
        RemovePhoneNumberSucces,
        RemovePhoneNumberError,
        ConfirmedPhoneNumberSucces,
        ConfirmedPhoneNumberError,
        AdminSendConfirmationEmailSucces,
        AdminSendConfirmationEmailError,
        AdminDeleteAccountSucces,
        AdminDeleteAccountError,
        UserDeleteAccountSucces
    }
}
