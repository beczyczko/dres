//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class PumlService {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:7090";
    }

    asPumlFile(specIds: string[] | undefined): Observable<string> {
        let url_ = this.baseUrl + "/api/puml/combine?";
        if (specIds === null)
            throw new Error("The parameter 'specIds' cannot be null.");
        else if (specIds !== undefined)
            specIds && specIds.forEach(item => { url_ += "specIds=" + encodeURIComponent("" + item) + "&"; });
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAsPumlFile(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAsPumlFile(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<string>;
                }
            } else
                return _observableThrow(response_) as any as Observable<string>;
        }));
    }

    protected processAsPumlFile(response: HttpResponseBase): Observable<string> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as string;
            return _observableOf(result200);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result500: any = null;
            result500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    downloadPumlFile(specIds: string[] | undefined): Observable<FileResponse> {
        let url_ = this.baseUrl + "/api/puml/combine/download-puml-file?";
        if (specIds === null)
            throw new Error("The parameter 'specIds' cannot be null.");
        else if (specIds !== undefined)
            specIds && specIds.forEach(item => { url_ += "specIds=" + encodeURIComponent("" + item) + "&"; });
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/octet-stream"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDownloadPumlFile(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDownloadPumlFile(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<FileResponse>;
                }
            } else
                return _observableThrow(response_) as any as Observable<FileResponse>;
        }));
    }

    protected processDownloadPumlFile(response: HttpResponseBase): Observable<FileResponse> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers.get("content-disposition") : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return _observableOf({ fileName: fileName, data: responseBlob as any, status: status, headers: _headers });
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result500: any = null;
            result500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    svg(specIds: string[] | undefined): Observable<string> {
        let url_ = this.baseUrl + "/api/puml/combine/svg?";
        if (specIds === null)
            throw new Error("The parameter 'specIds' cannot be null.");
        else if (specIds !== undefined)
            specIds && specIds.forEach(item => { url_ += "specIds=" + encodeURIComponent("" + item) + "&"; });
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processSvg(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processSvg(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<string>;
                }
            } else
                return _observableThrow(response_) as any as Observable<string>;
        }));
    }

    protected processSvg(response: HttpResponseBase): Observable<string> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as string;
            return _observableOf(result200);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result500: any = null;
            result500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

@Injectable()
export class SpecificationsService {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:7090";
    }

    all(): Observable<SpecificationAo[]> {
        let url_ = this.baseUrl + "/api/specifications";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAll(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAll(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<SpecificationAo[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<SpecificationAo[]>;
        }));
    }

    protected processAll(response: HttpResponseBase): Observable<SpecificationAo[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as SpecificationAo[];
            return _observableOf(result200);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result500: any = null;
            result500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    publish(specificationData: Specification): Observable<SpecificationAo> {
        let url_ = this.baseUrl + "/api/specifications";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(specificationData);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPublish(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPublish(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<SpecificationAo>;
                }
            } else
                return _observableThrow(response_) as any as Observable<SpecificationAo>;
        }));
    }

    protected processPublish(response: HttpResponseBase): Observable<SpecificationAo> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as SpecificationAo;
            return _observableOf(result200);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result500: any = null;
            result500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    details(id: number): Observable<SpecificationAo> {
        let url_ = this.baseUrl + "/api/specifications/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDetails(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDetails(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<SpecificationAo>;
                }
            } else
                return _observableThrow(response_) as any as Observable<SpecificationAo>;
        }));
    }

    protected processDetails(response: HttpResponseBase): Observable<SpecificationAo> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as SpecificationAo;
            return _observableOf(result200);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result404: any = null;
            result404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result500: any = null;
            result500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

export interface ProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;

    [key: string]: any;
}

export interface SpecificationAo {
    specificationId: SpecificationId;
    createdOn: Date;
    resources: ResourceAo[];
}

export interface SpecificationId {
    name: string;
    tag: string;
    value: string;
}

export interface ResourceAo {
    name: string;
    domainContext: string;
    identifier: string;
    type: string;
    properties: PropertyAo[];
}

export interface PropertyAo {
    name: string;
    type: string;
    relatedResourcesIdentifiers: string[];
}

export interface Specification {
    specificationId: SpecificationId;
    dresApiVersion: string;
    resources: Resource[];
}

export interface Resource {
    name?: string;
    domainContext?: string;
    identifier?: string;
    type?: string;
    properties?: Property[];
}

export interface Property {
    resourceIdentifier?: string;
    name?: string;
    type?: string;
    relatedResourcesIdentifiers?: string[];
}

export interface FileResponse {
    data: Blob;
    status: number;
    fileName?: string;
    headers?: { [name: string]: any };
}

export class ApiException extends Error {
    override message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}