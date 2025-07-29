import { Component } from '@angular/core';
import { RoleBasedActionsComponent } from '../../components/role-based-actions/role-based-actions.component';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [RoleBasedActionsComponent],
  templateUrl: './student.component.html',
  styleUrl: './student.component.css'
})
export class StudentComponent {

}
