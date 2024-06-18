import { Component, ViewChild, ViewContainerRef } from "@angular/core";
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "../shared/services/auth.service";
import { exhaustMap, take, tap } from "rxjs/operators";
import { ButtonComponent } from "../shared/components/button/button.component";
import { Observable } from "rxjs";
import { User } from "../users/models/user.model";

@Component({
  selector: "app-login-register",
  templateUrl: "./login-register.component.html",
})
export class LoginRegisterComponent {
  form: UntypedFormGroup;
  loginInProgress = false;

  constructor(
    public router: Router,
    public authService: AuthService,
    fb: UntypedFormBuilder
  ) {
    this.form = fb.group({
      email: new UntypedFormControl("", [Validators.required]),
      password: new UntypedFormControl("", [Validators.required]),
    });
  }

  get email(): string {
    return this.form.get("email").value;
  }
  get password(): string {
    return this.form.get("password").value;
  }

  @ViewChild("viewContainerRef", { read: ViewContainerRef })
  viewContainerRef: ViewContainerRef;

  handleLogin(): void {
    this.authService
      .login(this.email, this.password)
      .pipe(
        tap((_) => (this.loginInProgress = true)),
        take(1),
        exhaustMap((_) => this.getCurrentUser())
      )
      .subscribe(
        (_) => {
          this.loginInProgress = false;
          this.router.navigate(["/home"]);
        },
        (error) => console.error(error)
      );
  }

  tempLoadDynamicComponent(): void {
    const viewContainerRef = this.viewContainerRef;
    viewContainerRef.clear();
    const component = viewContainerRef.createComponent(ButtonComponent);
    component.instance.title = "Press me";
  }

  private getCurrentUser(): Observable<User> {
    return this.authService.getCurrentUser().pipe(take(1));
  }
}
