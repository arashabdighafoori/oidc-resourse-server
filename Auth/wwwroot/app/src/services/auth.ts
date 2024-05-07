export class AuthService {
  // expires!: Observable<number>;
  // expiresunsub!: Subscription;
  // constructor(private query: QueryService) {}
  // login(Username: string, Password: string) {
  //   return new Promise<EntityOrErrors<undefined>>((rs, rj) => {
  //     this.query
  //       .query(
  //         "tokenAuth",
  //         `mutation ($Username: String!, $Password: String!){ tokenAuth(username: $Username, password: $Password){ token refreshExpiresIn refreshToken } }`,
  //         { Username, Password }
  //       )
  //       .then((e: any) => {
  //         this.setExp();
  //         rs({
  //           is_success: !e.errors,
  //           errors: e.errors,
  //           data: undefined,
  //         });
  //       })
  //       .catch(rj);
  //   });
  // }
  // refresh_tonken() {
  //   return new Promise<EntityOrErrors<undefined>>((rs, rj) => {
  //     this.query
  //       .query(
  //         "refreshToken",
  //         `mutation { refreshToken { token refreshExpiresIn refreshToken } }`,
  //         {}
  //       )
  //       .then((e: any) => {
  //         this.setExp();
  //         rs({
  //           is_success: !e.errors,
  //           errors: e.errors,
  //           data: undefined,
  //         });
  //       })
  //       .catch(rj);
  //   });
  // }
  // async is_authed() {
  //   const me = await this.me();
  //   this.setExp();
  //   return me.is_success && me.data?.username;
  // }
  // me(repeat = true) {
  //   return new Promise<EntityOrErrors<IUser | undefined>>((rs, rj) => {
  //     this.query
  //       .query("me", `{ me { username email } }`, {})
  //       .then((e: any) => {
  //         const current = {
  //           is_success: !!e.data,
  //           errors: e.errors,
  //           data: e.data,
  //         };
  //         if (!current.is_success) {
  //           this.refresh_tonken().then((e) => {
  //             if (e.is_success) {
  //               if (repeat) this.me(false).then((d) => rs(d));
  //             } else rs({ is_success: false } as any);
  //           });
  //         } else rs(current);
  //       })
  //       .catch((err) => {
  //         rs({
  //           is_success: false,
  //           errors: [err],
  //           data: undefined,
  //         });
  //       });
  //   });
  // }
  // logout() {
  //   return new Promise<EntityOrErrors<undefined>>((rs, rj) => {
  //     this.query
  //       .query(
  //         "deleteTokenCookie",
  //         `mutation { deleteTokenCookie{ deleted } deleteRefreshTokenCookie{ deleted } deleteIsAuth } `,
  //         {}
  //       )
  //       .then((e: any) => {
  //         rs({
  //           is_success: !e.errors,
  //           errors: e.errors,
  //           data: undefined,
  //         });
  //       })
  //       .catch(rj);
  //   });
  // }
  // setExp() {
  //   // setExp(e: any) {
  //   // const token = JSON.parse(atob(e.data.token.split('.')[1]));
  //   if (this.expiresunsub) this.expiresunsub.unsubscribe();
  //   this.expires = interval(1000 * 60 * 4 + 500);
  //   // console.log(new Date());
  //   this.expiresunsub = this.expires.subscribe(() => {
  //     this.refresh_tonken();
  //   });
  // }
}
