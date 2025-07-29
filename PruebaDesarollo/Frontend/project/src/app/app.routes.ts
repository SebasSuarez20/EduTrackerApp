import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { LayoutMainComponent } from './layouts/layout-main/layout-main.component';
import { AuthLoginGuard } from './guards/authLogin.guard';
import { NotFoundComponent } from './components/not-found/not-found.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    canActivate:[AuthLoginGuard],
    loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'index',
    component: LayoutMainComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'Student',
        loadComponent: () => import('./pages/student/student.component').then(m => m.StudentComponent),
        runGuardsAndResolvers: 'paramsOrQueryParamsChange'
      },
      {
        path: 'Teacher',
        loadComponent: () => import('./pages/teacher/teacher.component').then(m => m.TeacherComponent),
        runGuardsAndResolvers: 'paramsOrQueryParamsChange'
      },
      {
        path: 'Administrator',
        loadComponent: () => import('./pages/administrator/administrator.component').then(m => m.AdministratorComponent),
        runGuardsAndResolvers: 'paramsOrQueryParamsChange'
      }
    ]
  },
  {
    path:"not-found",
    component:NotFoundComponent
  }
];