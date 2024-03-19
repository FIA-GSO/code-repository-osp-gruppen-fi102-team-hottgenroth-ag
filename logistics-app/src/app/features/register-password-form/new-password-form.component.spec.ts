import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPasswordFormComponent } from './new-password-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('NewPasswordFormComponent', () => {
  let component: NewPasswordFormComponent;
  let fixture: ComponentFixture<NewPasswordFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        NewPasswordFormComponent,
        BrowserAnimationsModule
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NewPasswordFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('validation works', () => {
    expect(component.passwordForm.controls["passwordFormControl"]).toBeDefined();
    expect(component.passwordForm.controls["passwordConfirmFormControl"]).toBeDefined();

    component.passwordForm.controls["passwordFormControl"].setValue("12345");
    component.passwordForm.controls["passwordConfirmFormControl"].setValue("1234");
    expect(component.passwordForm.valid).toBeFalse();

    component.passwordForm.controls["passwordConfirmFormControl"].setValue("12345");
    expect(component.passwordForm.valid).toBeTruthy();
  });
});
