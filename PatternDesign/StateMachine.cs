namespace PatternDesign;

enum IdentifyState
{
    Register,
    Login,
    LoginError,
    ForgotPassword,
    Successes,
}

enum MenuState
{
    Withdraw,
    Deposit,
    Transfer,
    History,
    Account,
    Logout,
}

enum WithdrawState
{
    OnWithdraw,
    OnWithdrawError,
    OnWithdrawSuccesses,
}

enum DepositState
{
    OnDeposit,
    OnDepositError,
    OnDepositSuccesses,
}

enum TransferState
{
    OnTransfer,
    OnTransferError,
    OnTransferSuccesses,
}

enum HistoryState
{
    ShowHistory,
    Exit
}

enum AccountState
{
    AccountStatus,
    ChangeName,
    ChangeUsername,
    ChangePassword,
    ChangePin,
    DeleteAccount,
    Exit,
}