import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
@Component({
  selector: 'app-run-trasaction-form',
  templateUrl: './run-trasaction-form.component.html',
  styleUrls: ['./run-trasaction-form.component.css']
})
export class RunTrasactionFormComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  runTransaction(f: NgForm) {
  console.log(f.value)
  }
}
