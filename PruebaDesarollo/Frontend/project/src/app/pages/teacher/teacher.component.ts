import { Component } from '@angular/core';
import { RoleBasedActionsComponent } from "../../components/role-based-actions/role-based-actions.component";

@Component({
  selector: 'app-teacher',
  standalone: true,
  imports: [RoleBasedActionsComponent],
  templateUrl: './teacher.component.html',
  styleUrl: './teacher.component.css'
})
export class TeacherComponent {

}
