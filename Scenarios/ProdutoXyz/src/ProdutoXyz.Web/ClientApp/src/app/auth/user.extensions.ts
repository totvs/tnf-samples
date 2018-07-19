import { User } from "oidc-client"

export interface Permission {
    key: string;
    value: boolean;
}

export interface UserWithPermission extends User {
    permissions: Permission[];
    hasPermission: (this: UserWithPermission, permission: string) => boolean;
}

export class UserPermission {
    public static AddPremissionFeatures(user: User): UserWithPermission {
        var userWithPermissions = user as UserWithPermission;

        userWithPermissions.permissions = [];
        userWithPermissions.hasPermission = this.hasPermission;

        return userWithPermissions;
    }

    public static hasPermission(this: UserWithPermission, permission: string): boolean {
        var permissions = this.permissions || [];

        var currentPermission = permissions.find(p => p.key.toLowerCase() === permission.toLowerCase());

        if (currentPermission) {
            return currentPermission.value;
        }

        return false;
    }
}
