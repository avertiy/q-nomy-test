import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Client } from '../client';
import { ClientService } from '../client.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-add-client',
  templateUrl: './add-client.component.html',
  styleUrls: ['./add-client.component.css']
})
export class AddClientComponent {

  @Output() add: EventEmitter<string> = new EventEmitter();

  addClientForm = this.formBuilder.group({
    name: ''
    });

  constructor(
    private clientService: ClientService,
    private formBuilder: FormBuilder,
  ) { }

  onSubmit(): void {
    // Process add client
    const name = this.addClientForm.value.name;
    this.add.emit(name);
    this.addClientForm.reset();
  }

}
