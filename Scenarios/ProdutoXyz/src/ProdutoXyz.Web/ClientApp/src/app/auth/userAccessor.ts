import { UserWithPermission } from "./user.extensions";

export class userAccessor {
    private static currentUser: UserWithPermission;

    static setCurrentUser(user: UserWithPermission): void {
        this.currentUser = user;
    }

    static getCurrentUser(): UserWithPermission {
        return this.currentUser;
    }
}
