import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";

import { ToastrModule } from "ngx-toastr";
import { CommonModule } from "@angular/common";
import { TokenInterceptor } from "./core/interceptors/token.interceptor";
import { SharedModule } from "./shared/shared.module";
import { AngularFireModule } from "@angular/fire/compat";
import { AngularFireStorageModule } from "@angular/fire/compat/storage";
import { environment } from "src/environments/environment.development";
import { JwtHelperService } from "@auth0/angular-jwt";

@NgModule({
	declarations: [AppComponent],
	imports: [
		BrowserModule,
		AppRoutingModule,
		CommonModule,
		BrowserAnimationsModule,
		HttpClientModule,
		SharedModule,
		ToastrModule.forRoot({
			timeOut: 3000,
			positionClass: "toast-top-right",
			preventDuplicates: true,
		}),
		AngularFireModule.initializeApp(environment.firebaseConfig),
		AngularFireStorageModule,
	],
	providers: [
		{
			provide: HTTP_INTERCEPTORS,
			useClass: TokenInterceptor,
			multi: true,
		},
	],
	bootstrap: [AppComponent],
})
export class AppModule {}
