import { Test, TestingModule } from '@nestjs/testing';
import { RegistracionService } from './registracion.service';

describe('RegistracionService', () => {
  let service: RegistracionService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [RegistracionService],
    }).compile();

    service = module.get<RegistracionService>(RegistracionService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });
});
