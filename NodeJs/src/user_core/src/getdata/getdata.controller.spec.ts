import { Test, TestingModule } from '@nestjs/testing';
import { GetdataController } from './getdata.controller';
import { GetdataService } from './getdata.service';

describe('GetdataController', () => {
  let controller: GetdataController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [GetdataController],
      providers: [GetdataService],
    }).compile();

    controller = module.get<GetdataController>(GetdataController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
