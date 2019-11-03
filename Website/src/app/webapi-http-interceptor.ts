import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class WebApiHttpInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const apiReq = req.clone({
            url: `http://localhost:5000/${req.url}`, headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
        return next.handle(apiReq);
    }
}
