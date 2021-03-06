import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { DialogWindowComponent } from './ui/controls/dialog-window/dialog-window.component';
import { AuthService } from './services/auth/auth.service';
import { WorkspaceModule } from './ui/pages/workspace/workspace.module';
import { routing } from './app.router.module';
import { LoginComponent } from './ui/controls/auth/login/login.component';
import { AuthCallbackComponent } from './ui/pages/auth/auth-callback/auth-callback.component';
import { FormsModule } from '@angular/forms';
import {
	MatIconModule,
	MatFormFieldModule,
	MatInputModule,
	MatButtonModule,
	MatDialogModule,
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginPageComponent } from './ui/pages/auth/login-page/login-page.component';
import { SilentRenewComponent } from './ui/pages/auth/silent-renew/silent-renew.component';
import { AuthInterceptor } from './utils/auth/auth.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ServiceWorkerModule } from '@angular/service-worker';

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		AuthCallbackComponent,
		LoginPageComponent,
		SilentRenewComponent,
	],
	entryComponents: [DialogWindowComponent],
	imports: [
		WorkspaceModule,
		FormsModule,
		BrowserAnimationsModule,
		MatIconModule,
		MatFormFieldModule,
		MatInputModule,
		MatButtonModule,
		MatDialogModule,
		routing,
		ServiceWorkerModule.register('ngsw-worker.js', { enabled: true }),
	],
	providers: [
		{
			provide: APP_INITIALIZER,
			useFactory: authProviderFactory,
			deps: [AuthService],
			multi: true,
		},
		{
			provide: HTTP_INTERCEPTORS,
			useClass: AuthInterceptor,
			multi: true
		  }
	],
	bootstrap: [AppComponent],
})
export class AppModule {}

export function authProviderFactory(provider: AuthService) {
	return () => provider.loadUser();
}
