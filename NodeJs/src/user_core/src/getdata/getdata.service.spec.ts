import { Test, TestingModule } from '@nestjs/testing';
import { GetdataService } from './getdata.service';

describe('GetdataService', () => {
  let service: GetdataService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [GetdataService],
    }).compile();

    service = module.get<GetdataService>(GetdataService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });
});
