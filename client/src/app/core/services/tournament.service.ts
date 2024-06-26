import { Injectable } from "@angular/core";
import { MasterService } from "./master/master.service";
import { TournamentApi } from "../constant/api/tournament.api";
import { Observable } from "rxjs";
import { Tournament } from "../models/classes/Tournament";
import { HttpParams } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class TournamentService {
	endpoints = TournamentApi;
	constructor(private master: MasterService) {}

	// Get All Tour
	getAll(
		currentPage: number = 1,
		pageSize: number = 5,
		searchTerm: string = "",
		sortCol: string = "",
		ascSort: string = "true"
	): Observable<any> {
		const params = new HttpParams()
			.set("currentPage", currentPage)
			.set("pageSize", pageSize)
			.set("searchTerm", searchTerm)
			.set("sortColumn", sortCol)
			.set("asc", ascSort);

		return this.master.get(this.endpoints.getAll, { params });
	}
	getAllNoPagi(): Observable<any> {
		return this.master.get(this.endpoints.getAll);
	}
	getCount(): Observable<any> {
		return this.master.get(this.endpoints.getAll);
	}

	// Get list by userId
	getList(id: number): Observable<Tournament[]> {
		const params = new HttpParams().set("userId", id);

		return this.master.get(`${this.endpoints.getList}`, { params });
	}

	getById(id: number): Observable<Tournament> {
		return this.master.get(`${this.endpoints.getById}/${id}`);
	}

	create(tournament: Tournament): Observable<Tournament> {
		return this.master.post(`${this.endpoints.create}`, tournament);
	}

	updateById(tournament: Tournament): Observable<Tournament> {
		return this.master.put(`${this.endpoints.update}`, tournament);
	}

	updateView(tournament: Tournament): Observable<any> {
		return this.master.put(`${this.endpoints.updateView}`, tournament);
	}

	deleteById(id: number): Observable<Tournament> {
		return this.master.delete(`${this.endpoints.deleteById}/${id}`);
	}

	searchByName(searchTerm: string = " "): Observable<Tournament[]> {
		const params = new HttpParams().set("searchTerm", searchTerm);
		return this.master.get(`${this.endpoints.search}`, { params });
	}
}
