import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent implements OnInit {

    form: FormGroup;

    constructor(
		public router: Router,
		public authService: AuthService,
		fb: FormBuilder) {
		this.form = fb.group({
			email: new FormControl('', [Validators.required]),
			password: new FormControl('', [Validators.required])
		});
	}

	get email() { return this.form.get('email').value; }
	get password() { return this.form.get('password').value; }

    ngOnInit(): void {
    }

	handleLogin() {
		this.authService.login(this.email, this.password).subscribe(() => {
			console.log('navigate');
			this.router.navigate(['/languages']);
		},
		(error) => console.error(error));
	}

}
