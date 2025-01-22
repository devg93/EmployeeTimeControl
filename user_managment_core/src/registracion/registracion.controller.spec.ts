import { Test, TestingModule } from '@nestjs/testing';
import { RegistracionController } from './registracion.controller';
import { RegistracionService } from './registracion.service';

describe('RegistracionController', () => {
  let controller: RegistracionController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [RegistracionController],
      providers: [RegistracionService],
    }).compile();

    controller = module.get<RegistracionController>(RegistracionController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
