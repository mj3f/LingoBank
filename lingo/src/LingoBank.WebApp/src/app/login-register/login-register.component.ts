import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent implements OnInit {

    form: FormGroup;

    constructor(
		public authService: AuthService,
		fb: FormBuilder) {
		this.form = fb.group({
			username: new FormControl('', [Validators.required]),
			password: new FormControl('', [Validators.required])
		});
	}

	get username() { return this.form.get('username').value; }
	get password() { return this.form.get('password').value; }

    ngOnInit(): void {
    }

	handleLogin() {
		console.log('logging in????');
		this.authService.login(this.username, this.password).subscribe((token: string) => console.log('token = ', token));
	}

}
