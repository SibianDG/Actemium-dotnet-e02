﻿namespace _2021_dotnet_e_02.Models.Enums
{
    public enum UserStatus
    {
        /**
	 * The In request.
	 */
        // Customer has registered an account
        // The new customer account needs to be activated by the Admin
        // Once ativated the customer can login and request a contract
        IN_REQUEST

        // User is able to login
        ,
        /**
	 * Active user status.
	 */
        ACTIVE

        // UserAccount has been blocked after 5 consecutive unsuccesfull login attempts
        // User must contact Administrator (SysAdmin) to unblock account
        ,
        /**
	 * Blocked user status.
	 */
        BLOCKED

        // User account has been removed*
        // *Data is still kept in database
        ,
        /**
	 * Inactive user status.
	 */
        INACTIVE
    }
}