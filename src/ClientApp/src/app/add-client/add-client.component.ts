import { Component, Input } from '@angular/core';
import { Client } from '../client';
import { ClientService } from '../client.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-add-client',
  templateUrl: './add-client.component.html'
})
export class AddClientComponent {
  public name: string = '';
  public addClientForm = this.formBuilder.group({
    name: ''
    });

  constructor(
    private clientService: ClientService,
    private formBuilder: FormBuilder,
  ) { }

  onSubmit(): void {
    // Process add client
    this.clientService.addClient(this.name);
    console.warn('add client form has been submitted', this.addClientForm.value);
    this.addClientForm.reset();
  }

}
