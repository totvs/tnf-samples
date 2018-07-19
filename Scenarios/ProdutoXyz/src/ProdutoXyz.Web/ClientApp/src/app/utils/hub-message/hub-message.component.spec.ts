import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HubMessageComponent } from './hub-message.component';

describe('HubMessageComponent', () => {
  let component: HubMessageComponent;
  let fixture: ComponentFixture<HubMessageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HubMessageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HubMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
